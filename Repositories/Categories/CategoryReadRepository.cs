using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Categories.Interfaces;

namespace StartupLab.Repositories.Categories;

/// <summary>
/// EF Core implementation of <see cref="ICategoryReadRepository"/>.
/// </summary>
public class CategoryReadRepository(ApplicationDbContext context) : ICategoryReadRepository
{
    public async Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken) =>
        await context.Categories
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

    public async Task<IReadOnlyList<Category>> GetActiveAsync(int max, CancellationToken cancellationToken) =>
        await context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.Title)
            .ThenBy(c => c.Id)
            .Take(max)
            .ToListAsync(cancellationToken);
}
