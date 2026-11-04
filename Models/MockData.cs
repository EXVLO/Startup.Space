namespace StartupLab.Models;

/// <summary>
/// Sample data for the page mockups, until the services exist.
/// </summary>
public static class MockData
{
    public static IReadOnlyList<MockStartup> Startups { get; } =
    [
        new(1, "GreenRoute", "Carbon-aware delivery routes for small couriers.", "Climate", 1),
        new(2, "Lingo Loop", "Daily five-minute language practice with friends.", "EdTech", 2),
        new(3, "Medibox", "Smart pill box that reminds and reports to family.", "Health", 3),
        new(4, "Stackshelf", "Inventory tracking for tiny online shops.", "Retail", 1),
        new(5, "Fundly", "Micro-investing round-ups for students.", "FinTech", 4),
        new(6, "Campus Eats", "Pre-order lunch from campus cafés, skip the queue.", "Food", 5),
        new(7, "Pathfinder AI", "Career path suggestions from your course history.", "AI", 2)
    ];

    public static IReadOnlyList<MockPerson> Founders { get; } =
    [
        new(1, "Nino Beridze", "Founder of GreenRoute and Stackshelf", "Logistics engineer who loves making cities cleaner."),
        new(2, "Luka Kapanadze", "Founder of Lingo Loop and Pathfinder AI", "Data science student building learning tools."),
        new(3, "Mariam Gelashvili", "Founder of Medibox", "Biomedical student focused on elderly care."),
        new(4, "Giorgi Tsiklauri", "Founder of Fundly", "Finance nerd making investing simple for students."),
        new(5, "Ana Lomidze", "Founder of Campus Eats", "Designer who hates waiting in lines."),
        new(6, "Saba Javakhishvili", "Founder", "Exploring ideas in mobility and travel.")
    ];

    public static IReadOnlyList<MockPerson> Mentors { get; } =
    [
        new(101, "Elena Ricci", "Mentor · Product", "Ten years building consumer apps in Berlin and Milan."),
        new(102, "David Chen", "Mentor · Fundraising", "Angel investor and former VC associate."),
        new(103, "Tamar Abashidze", "Mentor · Marketing", "Growth lead at two scaled startups."),
        new(104, "Omar Haddad", "Mentor · Engineering", "CTO who has hired and led 40+ engineers.")
    ];

    public static IReadOnlyList<MockNews> News { get; } =
    [
        new(1, "Demo Day 2026 dates announced", "Twelve teams will pitch to investors on December 12.", "Events", "Oct 27, 2026"),
        new(2, "Medibox wins Health Hack", "The smart pill box took first place among 40 teams.", "Startups", "Oct 20, 2026"),
        new(3, "New mentor: Omar Haddad", "Book office hours on engineering and hiring.", "Community", "Oct 15, 2026"),
        new(4, "Fundraising workshop recap", "Slides and notes from David Chen's session.", "Learning", "Oct 9, 2026"),
        new(5, "Residency applications open", "Apply for the spring cohort before November 30.", "Program", "Oct 2, 2026"),
        new(6, "Campus Eats launches pilot", "Three campus cafés now accept pre-orders.", "Startups", "Sep 28, 2026")
    ];
}

public record MockStartup(int Id, string Name, string Tagline, string Category, int FounderId)
{
    public MockPerson Founder => MockData.Founders.Single(f => f.Id == FounderId);
}

public record MockPerson(int Id, string Name, string Role, string Bio)
{
    public string Initials => string.Concat(Name.Split(' ').Select(part => part[0]));
}

public record MockNews(int Id, string Title, string Summary, string Tag, string Date);
