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

    public async Task<IReadOnlyList<Category>> GetCategoriesAsync(bool isActive, int skip, int take, CancellationToken cancellationToken) =>
        await context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive == isActive)
            .OrderBy(c => c.Title)
            .ThenBy(c => c.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    public async Task<int> CountCategoriesAsync(bool isActive, CancellationToken cancellationToken) =>
        await context.Categories.CountAsync(c => c.IsActive == isActive, cancellationToken);
}
