using StartupLab.Repositories.NewsItems;
using StartupLab.Repositories.NewsItems.Interfaces;
using StartupLab.Repositories.NewsPhotos;
using StartupLab.Repositories.NewsPhotos.Interfaces;
using StartupLab.Repositories.NewsVideos;
using StartupLab.Repositories.NewsVideos.Interfaces;
using StartupLab.Repositories.StartupChecklists;
using StartupLab.Repositories.StartupChecklists.Interfaces;
using StartupLab.Repositories.StartupDocuments;
using StartupLab.Repositories.StartupDocuments.Interfaces;
using StartupLab.Repositories.StartupLinks;
using StartupLab.Repositories.StartupLinks.Interfaces;
using StartupLab.Repositories.StartupPhotos;
using StartupLab.Repositories.StartupPhotos.Interfaces;
using StartupLab.Repositories.Startups;
using StartupLab.Repositories.Startups.Interfaces;
using StartupLab.Repositories.StartupVideos;
using StartupLab.Repositories.StartupVideos.Interfaces;
using StartupLab.Repositories.ToolkitChecklists;
using StartupLab.Repositories.ToolkitChecklists.Interfaces;
using StartupLab.Repositories.UserContacts;
using StartupLab.Repositories.UserContacts.Interfaces;
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

        services.AddScoped<IUserContactReadRepository, UserContactReadRepository>();
        services.AddScoped<IUserContactWriteRepository, UserContactWriteRepository>();

        services.AddScoped<IStartupReadRepository, StartupReadRepository>();
        services.AddScoped<IStartupWriteRepository, StartupWriteRepository>();

        services.AddScoped<IStartupPhotoReadRepository, StartupPhotoReadRepository>();
        services.AddScoped<IStartupPhotoWriteRepository, StartupPhotoWriteRepository>();

        services.AddScoped<IStartupVideoReadRepository, StartupVideoReadRepository>();
        services.AddScoped<IStartupVideoWriteRepository, StartupVideoWriteRepository>();

        services.AddScoped<IStartupDocumentReadRepository, StartupDocumentReadRepository>();
        services.AddScoped<IStartupDocumentWriteRepository, StartupDocumentWriteRepository>();

        services.AddScoped<IStartupLinkReadRepository, StartupLinkReadRepository>();
        services.AddScoped<IStartupLinkWriteRepository, StartupLinkWriteRepository>();

        services.AddScoped<INewsReadRepository, NewsReadRepository>();
        services.AddScoped<INewsWriteRepository, NewsWriteRepository>();

        services.AddScoped<INewsPhotoReadRepository, NewsPhotoReadRepository>();
        services.AddScoped<INewsPhotoWriteRepository, NewsPhotoWriteRepository>();

        services.AddScoped<INewsVideoReadRepository, NewsVideoReadRepository>();
        services.AddScoped<INewsVideoWriteRepository, NewsVideoWriteRepository>();

        services.AddScoped<IToolkitChecklistReadRepository, ToolkitChecklistReadRepository>();
        services.AddScoped<IToolkitChecklistWriteRepository, ToolkitChecklistWriteRepository>();

        services.AddScoped<IStartupChecklistReadRepository, StartupChecklistReadRepository>();
        services.AddScoped<IStartupChecklistWriteRepository, StartupChecklistWriteRepository>();

        return services;
    }
}
