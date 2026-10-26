using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StartupLab.Models.Entities;

namespace StartupLab.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityUserContext<User, int>(options)
{
    public DbSet<UserPhoto> UserPhotos => Set<UserPhoto>();

    public DbSet<UserContact> UserContacts => Set<UserContact>();

    public DbSet<UserExperience> UserExperiences => Set<UserExperience>();

    public DbSet<UserStartup> UserStartups => Set<UserStartup>();

    public DbSet<UserChecklist> UserChecklists => Set<UserChecklist>();

    public DbSet<Startup> Startups => Set<Startup>();

    public DbSet<StartupPhoto> StartupPhotos => Set<StartupPhoto>();

    public DbSet<StartupVideo> StartupVideos => Set<StartupVideo>();

    public DbSet<StartupDocument> StartupDocuments => Set<StartupDocument>();

    public DbSet<StartupLink> StartupLinks => Set<StartupLink>();

    public DbSet<StartupCategory> StartupCategories => Set<StartupCategory>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<News> News => Set<News>();

    public DbSet<NewsPhoto> NewsPhotos => Set<NewsPhoto>();

    public DbSet<NewsVideo> NewsVideos => Set<NewsVideo>();

    public DbSet<NewsTag> NewsTags => Set<NewsTag>();

    public DbSet<ToolkitVideo> ToolkitVideos => Set<ToolkitVideo>();

    public DbSet<ToolkitDocument> ToolkitDocuments => Set<ToolkitDocument>();

    public DbSet<ToolkitGlossary> ToolkitGlossaries => Set<ToolkitGlossary>();

    public DbSet<ToolkitChecklist> ToolkitChecklists => Set<ToolkitChecklist>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
