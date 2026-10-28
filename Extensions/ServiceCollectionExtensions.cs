using StartupLab.Repositories.UserPhotos;
using StartupLab.Repositories.UserPhotos.Interfaces;
using StartupLab.Repositories.Users;
using StartupLab.Repositories.Users.Interfaces;

namespace StartupLab.Extensions;

/// <summary>
/// Registers the app's repositories and services for dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds every repository and service. Call it after the database is registered.
    /// Everything is scoped, because repositories use the scoped DbContext.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        services.AddScoped<IUserPhotoReadRepository, UserPhotoReadRepository>();
        services.AddScoped<IUserPhotoWriteRepository, UserPhotoWriteRepository>();

        return services;
    }
}
