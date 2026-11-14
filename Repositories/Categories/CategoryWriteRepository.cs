using Microsoft.EntityFrameworkCore;
using StartupLab.Data;
using StartupLab.Models.Entities;
using StartupLab.Repositories.Categories.Interfaces;

namespace StartupLab.Repositories.Categories;

/// <summary>
/// EF Core implementation of <see cref="ICategoryWriteRepository"/>.
/// </summary>
public class CategoryWriteRepository(ApplicationDbContext context) : ICategoryWriteRepository
{
    public async Task<Category> CreateAsync(string title, string? description, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Title = title,
            Description = description,
            DateCreated = DateTime.UtcNow
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<bool> UpdateAsync(int categoryId, string title, string? description, CancellationToken cancellationToken) =>
        await context.Categories
            .Where(c => c.Id == categoryId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Title, title)
                .SetProperty(c => c.Description, description)
                .SetProperty(c => c.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;

    public async Task<bool> ActivateAsync(int categoryId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(categoryId, true, cancellationToken);

    public async Task<bool> DeactivateAsync(int categoryId, CancellationToken cancellationToken) =>
        await UpdateIsActiveAsync(categoryId, false, cancellationToken);

    private async Task<bool> UpdateIsActiveAsync(int categoryId, bool isActive, CancellationToken cancellationToken) =>
        await context.Categories
            .Where(c => c.Id == categoryId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.IsActive, isActive)
                .SetProperty(c => c.DateModified, DateTime.UtcNow),
                cancellationToken) > 0;
}
