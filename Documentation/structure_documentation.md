# Project Structure Documentation

> **Status:** Draft v0.7. This is the planned structure. Built so far: the data model (`Models/Entities/`, `Data/`), the `Repositories/Users/` and `Repositories/UserPhotos/` groups, and dependency injection (`Extensions/`). Everything else is still planned.
> Requirements, data model, and open questions live in [`overview_documentation.md`](overview_documentation.md). Question IDs (for example Q-BOOK-3) refer to the Open Questions section there.
> How code is written (comments, naming, spacing) is in [`coding_style_documentation.md`](coding_style_documentation.md).

---

The layer order follows Section 3.1 of the overview: **Controller -> Service -> Repository -> EF Core (DbContext) -> PostgreSQL**, with Razor views rendered by controllers.

## 1. Conventions

- **Namespaces start with `StartupLab`** and follow the folder (`StartupLab.Services`, `StartupLab.Models.Entities`). Don't use `Startup.Lab`: a root namespace named `Startup` hides the `Startup` entity class (error CS0118).
- **One class or interface per file.** The file name matches the type name (`BookingService.cs` contains `BookingService`).
- **Interfaces in an `Interfaces/` subfolder** inside `Services/` and inside `Repositories/`. Each implementation has its own interface (`IBookingService` / `BookingService`).
- **Controllers depend only on service interfaces.** Services depend only on repository interfaces. Repositories are the only layer that uses the `DbContext`.
- **Entities (database tables) and ViewModels (page data) are separate.** Views never receive entities directly.
- **One folder per main entity group in `Repositories/`** (for example `Startups/` also handles Startup Photo/Video/Document/Link). This avoids one repository per small table.
- **Each group has a read repository and a write repository** (`UserReadRepository` / `UserWriteRepository`), each with its own interface in the group's `Interfaces/` subfolder. Read repositories only run no-tracking queries. A service depends only on the side it needs, so read-only services never see write methods.
- **A group folder must not have the same name as an entity.** A folder named `News` creates the namespace `StartupLab.Repositories.News`, which hides the `News` entity (error CS0118, same as `Startup` above). The News group therefore uses `NewsItems/`.
- **Dependency injection:** every interface/implementation pair is registered in `AddApplicationServices` in `Extensions/ServiceCollectionExtensions.cs`, and `Program.cs` calls it after registering the database. Everything is registered as scoped, because repositories use the scoped `DbContext`.

## 2. Folder tree

Items marked **(pending)** depend on an open question and may change or be removed.

```
Startup.Lab/
├── Startup.Lab.csproj               # RootNamespace = StartupLab
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
│   ├── ProfileController.cs         # /Profile
│   ├── MyStartupsController.cs      # /MyStartups (the logged-in user's startups)
│   ├── SettingsController.cs        # /Settings
│   ├── BookingController.cs         # /Booking (users book a mentor); mentor booking page (pending: Q-BOOK-14)
│   ├── AdminController.cs           # give mentor role to a user (pending: route and other admin pages, Q-ROLE-4)
│   ├── ArchiveController.cs         # /Archive
│   └── ToolkitController.cs         # /Toolkit, /Toolkit/Videos, /Toolkit/Pathway
│
├── Models/
│   ├── Entities/                    # one class per database table (Section 6)
│   │   ├── User.cs                  # includes IsMentor, IsAdmin (no IsFounder)
│   │   ├── UserPhoto.cs
│   │   ├── UserContact.cs
│   │   ├── UserExperience.cs
│   │   ├── Startup.cs
│   │   ├── StartupPhoto.cs
│   │   ├── StartupVideo.cs
│   │   ├── StartupDocument.cs
│   │   ├── StartupLink.cs
│   │   ├── Category.cs
│   │   ├── StartupCategory.cs       # join table
│   │   ├── Tag.cs
│   │   ├── NewsTag.cs               # join table
│   │   ├── UserStartup.cs           # join table
│   │   ├── News.cs
│   │   ├── NewsPhoto.cs
│   │   ├── NewsVideo.cs
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
│       │   └── StartupFormViewModel.cs      # create/edit from My Startups (pending: Q-PR-2)
│       ├── MyStartups/
│       │   └── MyStartupsViewModel.cs       # own startup cards, search, tag filter (pending: Q-SU-14)
│       ├── Founders/
│       │   ├── FounderListViewModel.cs
│       │   ├── FounderCardViewModel.cs
│       │   └── FounderDetailsViewModel.cs
│       ├── News/
│       │   ├── NewsListViewModel.cs
│       │   └── NewsCardViewModel.cs
│       ├── AboutUs/
│       │   ├── AboutUsViewModel.cs
│       │   └── MentorCardViewModel.cs       # also used by Landing "Meet the Crew"
│       ├── Booking/
│       │   ├── BookingViewModel.cs
│       │   └── MentorBookingViewModel.cs    # mentor booking page (pending: Q-BOOK-14)
│       ├── Admin/
│       │   └── UserRoleViewModel.cs         # list users, toggle mentor role (pending: Q-ROLE-4)
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
│   │   ├── IMentorService.cs
│   │   ├── IAdminService.cs
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
│   ├── MentorService.cs
│   ├── AdminService.cs
│   ├── CategoryService.cs
│   ├── ToolkitService.cs
│   ├── ChecklistService.cs
│   ├── BookingService.cs
│   ├── ArchiveService.cs
│   └── FileStorageService.cs        # (pending: Q-DATA-2)
│
├── Repositories/                    # one folder per entity group; every group has the same four files as Users/
│   ├── Users/                       # User, UserContact, UserExperience (mentors are users with IsMentor)
│   │   ├── Interfaces/
│   │   │   ├── IUserReadRepository.cs
│   │   │   └── IUserWriteRepository.cs
│   │   ├── UserReadRepository.cs
│   │   └── UserWriteRepository.cs
│   ├── UserPhotos/                  # UserPhoto (one photo per user; set returns the old address_key so the file can be deleted)
│   │   ├── Interfaces/
│   │   │   ├── IUserPhotoReadRepository.cs
│   │   │   └── IUserPhotoWriteRepository.cs
│   │   ├── UserPhotoReadRepository.cs
│   │   └── UserPhotoWriteRepository.cs
│   ├── Startups/                    # Startup, Photo, Video, Document, Link, UserStartup, StartupCategory
│   ├── NewsItems/                   # News, NewsPhoto, NewsVideo, NewsTag (not "News/", see Conventions)
│   ├── Categories/                  # Category, Tag
│   ├── Toolkit/                     # ToolkitVideo, ToolkitDocument, ToolkitGlossary
│   ├── Checklists/                  # ToolkitChecklist, UserChecklist
│   └── Bookings/                    # (pending: Q-BOOK-3)
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
│   └── ServiceCollectionExtensions.cs   # AddApplicationServices: registers all services and repositories for DI
│
├── Views/
│   ├── Shared/                      # _Layout (Header, Nav Bar, Footer), Error, partials
│   ├── Home/                        # Index (Landing), Privacy
│   ├── Account/                     # Login, Register, ForgotPassword
│   ├── Startups/                    # Index, Details
│   ├── Founders/                    # Index, Details
│   ├── News/                        # Index
│   ├── AboutUs/                     # Index, Team
│   ├── Profile/                     # Index
│   ├── MyStartups/                  # Index
│   ├── Settings/                    # Index
│   ├── Booking/                     # Index, Mentor (pending: Q-BOOK-14)
│   ├── Admin/                       # Users (give mentor role) (pending: Q-ROLE-4)
│   ├── Archive/                     # Index
│   └── Toolkit/                     # Index, Videos, Pathway, Checklist
│
└── wwwroot/                         # css, js, images, lib
```

## 3. What each service is responsible for

| Service | Responsibility | Uses repositories |
|---|---|---|
| `AccountService` | Login, register, logout, forgot password | `IUserReadRepository` (+ ASP.NET Core Identity if chosen, Q-TECH-3) |
| `UserService` | Profile data, photos, contacts, experience, settings | `IUserReadRepository`, `IUserWriteRepository`, `IUserPhotoReadRepository`, `IUserPhotoWriteRepository` |
| `StartupService` | Startup list (search, filters, pagination), details, create/edit, the user's own startups for My Startups | `IStartupReadRepository`, `IStartupWriteRepository`, `ICategoryReadRepository` |
| `FounderService` | Founder list and details (every user is a founder) | `IUserReadRepository`, `IStartupReadRepository` |
| `NewsService` | News list (pagination, tag filter), featured news (`is_primary`) | `INewsReadRepository`, `ICategoryReadRepository` |
| `MentorService` | Mentors (users with `IsMentor`) for About Us, Landing "Meet the Crew", and the booking page | `IUserReadRepository` |
| `AdminService` | Give the mentor role to any user (only admins). Other admin actions are pending (Q-ROLE-4) | `IUserReadRepository`, `IUserWriteRepository` |
| `CategoryService` | Categories (Startups) and tags (News) for filters | `ICategoryReadRepository` |
| `ToolkitService` | Toolkit videos, documents, glossary, pathways | `IToolkitReadRepository` |
| `ChecklistService` | Founder checklist and progress | `IChecklistReadRepository`, `IChecklistWriteRepository` |
| `BookingService` | Users booking mentors, mentor availability, mentor booking page (pending: Q-BOOK-14) | `IBookingReadRepository`, `IBookingWriteRepository`, `IUserReadRepository` |
| `ArchiveService` | Inactive startups and founders | `IStartupReadRepository`, `IUserReadRepository` |
| `FileStorageService` | Saving/reading files referenced by `address_key` | none (file storage, pending Q-DATA-2) |

## 4. Not in the structure yet

- **Investors:** optional in the Excel. No files until Q-INV-1 is answered.
- **Admin area:** only "give mentor role" is decided (`AdminController` / `AdminService` above). Other admin pages and the admin route are not defined (Q-ROLE-4).
- **Mentor booking page:** decided to exist (mentors only), but its content and route are WIP (Q-BOOK-14).
- **Founder Pathways / Requirements data:** no tables in the Excel yet (Q-TK-1, Q-TK-2). A proposed backend plan for Founder Pathways is in [`pathway_backend_plan.md`](pathway_backend_plan.md).
- **Test project:** a separate `Startup.Lab.Tests` project, once Q-TECH-7 is answered.

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-17 | Moved out of `overview_documentation.md` (was Section 9). | Aleksandre Nadirashvili |
| 0.2 | 2026-10-20 | Linked the Founder Pathways backend plan from Section 4. | Nikoloz Khidasheli |
| 0.3 | 2026-10-23 | Followed the new role model (overview Section 7.1). Removed `CrewMember`/`CrewMemberPhoto` entities and their service and repository. Added the `UserExperience` entity, `MentorService`, `AdminService`, `AdminController`, and the mentor booking page (pending). | Aleksandre Nadirashvili |
| 0.4 | 2026-10-24 | Synced with the updated Excel. Added the `Tag` and `NewsTag` entities. Added `MyStartupsController`, its view and view model (route `/MyStartups`), moved out of Profile. | Aleksandre Nadirashvili |
| 0.5 | 2026-10-24 | Project renamed from MentorBooking to Startup.Lab. Root folder and project file are `Startup.Lab`; C# namespaces start with `StartupLab` (see Conventions). | Aleksandre Nadirashvili |
| 0.6 | 2026-10-27 | Repositories split into one folder per entity group, each with a read and a write repository (Conventions, folder tree, service table). Added the first group, `Repositories/Users/`. The News group folder is `NewsItems/` to avoid hiding the `News` entity. | Aleksandre Nadirashvili |
| 0.7 | 2026-10-28 | Added the `Repositories/UserPhotos/` group; photo methods moved out of `Users/`. Dependency injection now lives in `AddApplicationServices` (`Extensions/ServiceCollectionExtensions.cs`), called from `Program.cs`. Status updated to list what is built. Linked the new `coding_style_documentation.md`. | Aleksandre Nadirashvili |
