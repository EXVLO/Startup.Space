# Project Structure Documentation

> **Status:** Draft v0.1. This is the planned structure only. No folders or files have been created yet.
> Requirements, data model, and open questions live in [`overview_documentation.md`](overview_documentation.md). Question IDs (for example Q-BOOK-3) refer to the Open Questions section there.

---

The layer order follows Section 3.1 of the overview: **Controller -> Service -> Repository -> EF Core (DbContext) -> PostgreSQL**, with Razor views rendered by controllers.

## 1. Conventions

- **One class or interface per file.** The file name matches the type name (`BookingService.cs` contains `BookingService`).
- **Interfaces in an `Interfaces/` subfolder** inside `Services/` and inside `Repositories/`. Each implementation has its own interface (`IBookingService` / `BookingService`).
- **Controllers depend only on service interfaces.** Services depend only on repository interfaces. Repositories are the only layer that uses the `DbContext`.
- **Entities (database tables) and ViewModels (page data) are separate.** Views never receive entities directly.
- **One repository per main entity group** (for example `StartupRepository` also handles Startup Photo/Video/Document/Link). This avoids one repository per small table.
- **Dependency injection:** every interface/implementation pair is registered in `Program.cs` (or in one `Extensions/ServiceCollectionExtensions.cs` file).

## 2. Folder tree

Items marked **(pending)** depend on an open question and may change or be removed.

```
MentorBooking/
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
│
├── Controllers/
│   ├── HomeController.cs            # /  (Landing), /PP (Privacy and Policy)
│   ├── AccountController.cs         # /Login, /Register, Logout, Forgot Password
│   ├── StartupsController.cs        # /Startups, /Startups/{id}
│   ├── FoundersController.cs        # /Founders, /Founders/{id}
│   ├── NewsController.cs            # /News
│   ├── AboutUsController.cs         # /AboutUs, /AboutUs/Team
│   ├── ProfileController.cs         # /Profile (profile, my startups)
│   ├── SettingsController.cs        # /Settings
│   ├── BookingController.cs         # /Booking
│   ├── ArchiveController.cs         # /Archive
│   └── ToolkitController.cs         # /Toolkit, /Toolkit/Videos, /Toolkit/Pathway
│
├── Models/
│   ├── Entities/                    # one class per database table (Section 6)
│   │   ├── User.cs
│   │   ├── UserPhoto.cs
│   │   ├── UserContact.cs
│   │   ├── Startup.cs
│   │   ├── StartupPhoto.cs
│   │   ├── StartupVideo.cs
│   │   ├── StartupDocument.cs
│   │   ├── StartupLink.cs
│   │   ├── Category.cs
│   │   ├── StartupCategory.cs       # join table
│   │   ├── UserStartup.cs           # join table
│   │   ├── News.cs
│   │   ├── NewsPhoto.cs
│   │   ├── NewsVideo.cs
│   │   ├── CrewMember.cs
│   │   ├── CrewMemberPhoto.cs
│   │   ├── ToolkitVideo.cs
│   │   ├── ToolkitDocument.cs
│   │   ├── ToolkitGlossary.cs
│   │   ├── ToolkitChecklist.cs
│   │   ├── UserChecklist.cs         # join table
│   │   └── Booking.cs               # (pending: Q-BOOK-3, no table in Excel yet)
│   │
│   └── ViewModels/                  # data passed to Razor views, grouped by page
│       ├── ErrorViewModel.cs        # already exists in Models/, will be moved here
│       ├── Account/
│       │   ├── LoginViewModel.cs
│       │   ├── RegisterViewModel.cs
│       │   └── ForgotPasswordViewModel.cs
│       ├── Home/
│       │   └── LandingViewModel.cs
│       ├── Startups/
│       │   ├── StartupListViewModel.cs
│       │   ├── StartupCardViewModel.cs
│       │   ├── StartupDetailsViewModel.cs
│       │   └── StartupFormViewModel.cs      # create/edit from Profile > My Startups (pending: Q-PR-2)
│       ├── Founders/
│       │   ├── FounderListViewModel.cs
│       │   ├── FounderCardViewModel.cs
│       │   └── FounderDetailsViewModel.cs
│       ├── News/
│       │   ├── NewsListViewModel.cs
│       │   └── NewsCardViewModel.cs
│       ├── AboutUs/
│       │   ├── AboutUsViewModel.cs
│       │   └── CrewMemberCardViewModel.cs
│       ├── Booking/
│       │   └── BookingViewModel.cs
│       ├── Archive/
│       │   └── ArchiveViewModel.cs
│       ├── Toolkit/
│       │   ├── ToolkitViewModel.cs
│       │   ├── ToolkitVideosViewModel.cs
│       │   ├── ToolkitPathwayViewModel.cs
│       │   └── ChecklistViewModel.cs
│       ├── Profile/
│       │   └── ProfileViewModel.cs
│       ├── Settings/
│       │   └── SettingsViewModel.cs
│       └── Shared/
│           └── PagedResultViewModel.cs      # pagination (Section 3.3 "Pagination and limits")
│
├── Services/
│   ├── Interfaces/
│   │   ├── IAccountService.cs
│   │   ├── IUserService.cs
│   │   ├── IStartupService.cs
│   │   ├── IFounderService.cs
│   │   ├── INewsService.cs
│   │   ├── ICrewMemberService.cs
│   │   ├── ICategoryService.cs
│   │   ├── IToolkitService.cs
│   │   ├── IChecklistService.cs
│   │   ├── IBookingService.cs
│   │   ├── IArchiveService.cs
│   │   └── IFileStorageService.cs   # (pending: Q-DATA-2, what address_key is)
│   ├── AccountService.cs
│   ├── UserService.cs
│   ├── StartupService.cs
│   ├── FounderService.cs
│   ├── NewsService.cs
│   ├── CrewMemberService.cs
│   ├── CategoryService.cs
│   ├── ToolkitService.cs
│   ├── ChecklistService.cs
│   ├── BookingService.cs
│   ├── ArchiveService.cs
│   └── FileStorageService.cs        # (pending: Q-DATA-2)
│
├── Repositories/
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   ├── IStartupRepository.cs
│   │   ├── INewsRepository.cs
│   │   ├── ICrewMemberRepository.cs
│   │   ├── ICategoryRepository.cs
│   │   ├── IToolkitRepository.cs
│   │   ├── IChecklistRepository.cs
│   │   └── IBookingRepository.cs    # (pending: Q-BOOK-3)
│   ├── UserRepository.cs            # User, UserPhoto, UserContact
│   ├── StartupRepository.cs         # Startup, Photo, Video, Document, Link, UserStartup, StartupCategory
│   ├── NewsRepository.cs            # News, NewsPhoto, NewsVideo
│   ├── CrewMemberRepository.cs      # CrewMember, CrewMemberPhoto
│   ├── CategoryRepository.cs        # Category
│   ├── ToolkitRepository.cs         # ToolkitVideo, ToolkitDocument, ToolkitGlossary
│   ├── ChecklistRepository.cs       # ToolkitChecklist, UserChecklist
│   └── BookingRepository.cs         # (pending: Q-BOOK-3)
│
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Configurations/              # EF Core Fluent API: keys, required fields, unique values, indexes
│   │   ├── UserConfiguration.cs
│   │   ├── StartupConfiguration.cs
│   │   └── ...                      # one file per entity
│   └── Migrations/                  # generated by EF Core, never edited by hand
│
├── Extensions/
│   └── ServiceCollectionExtensions.cs   # registers all services and repositories for DI
│
├── Views/
│   ├── Shared/                      # _Layout (Header, Nav Bar, Footer), Error, partials
│   ├── Home/                        # Index (Landing), Privacy
│   ├── Account/                     # Login, Register, ForgotPassword
│   ├── Startups/                    # Index, Details
│   ├── Founders/                    # Index, Details
│   ├── News/                        # Index
│   ├── AboutUs/                     # Index, Team
│   ├── Profile/                     # Index, MyStartups
│   ├── Settings/                    # Index
│   ├── Booking/                     # Index
│   ├── Archive/                     # Index
│   └── Toolkit/                     # Index, Videos, Pathway, Checklist
│
└── wwwroot/                         # css, js, images, lib
```

## 3. What each service is responsible for

| Service | Responsibility | Uses repositories |
|---|---|---|
| `AccountService` | Login, register, logout, forgot password | `IUserRepository` (+ ASP.NET Core Identity if chosen, Q-TECH-3) |
| `UserService` | Profile data, photos, contacts, settings | `IUserRepository` |
| `StartupService` | Startup list (search, filters, pagination), details, create/edit | `IStartupRepository`, `ICategoryRepository` |
| `FounderService` | Founder list and details (users who are founders) | `IUserRepository`, `IStartupRepository` |
| `NewsService` | News list (pagination, tags), featured news | `INewsRepository` |
| `CrewMemberService` | Crew / team for About Us and Landing | `ICrewMemberRepository` |
| `CategoryService` | Categories / tags for filters | `ICategoryRepository` |
| `ToolkitService` | Toolkit videos, documents, glossary, pathways | `IToolkitRepository` |
| `ChecklistService` | Founder checklist and progress | `IChecklistRepository` |
| `BookingService` | Sessions with crew/mentors, availability | `IBookingRepository`, `ICrewMemberRepository` |
| `ArchiveService` | Inactive startups and founders | `IStartupRepository`, `IUserRepository` |
| `FileStorageService` | Saving/reading files referenced by `address_key` | none (file storage, pending Q-DATA-2) |

## 4. Not in the structure yet

- **Investors:** optional in the Excel. No files until Q-INV-1 is answered.
- **Admin area:** `is_admin` exists, but no admin pages are defined (Q-ROLE-4).
- **Founder Pathways / Requirements data:** no tables yet (Q-TK-1, Q-TK-2).
- **Test project:** a separate `MentorBooking.Tests` project, once Q-TECH-7 is answered.

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-17 | Moved out of `overview_documentation.md` (was Section 9). | Aleksandre Nadirashvili |
