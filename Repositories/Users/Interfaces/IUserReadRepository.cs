using StartupLab.Models.Entities;

namespace StartupLab.Repositories.Users.Interfaces;

public interface IUserReadRepository
{
    Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);

    Task<User?> GetProfileAsync(int userId, CancellationToken cancellationToken);

    Task<IReadOnlyList<User>> GetUsersAsync(bool isActive, int skip, int take, CancellationToken cancellationToken);

    Task<int> CountUsersAsync(bool isActive, CancellationToken cancellationToken);

    Task<IReadOnlyList<User>> GetMentorsAsync(int max, CancellationToken cancellationToken);
}
