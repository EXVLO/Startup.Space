# Project Structure Documentation

> **Status:** Draft v0.20. This is the planned structure. Built so far: the data model (`Models/Entities/`, `Data/`), the repository groups, `Services/Users/` (`IUserService`), `Services/Account/` (`IAccountService`), `Controllers/Home/`, `Controllers/Account/` (`/Login`, `/Register`, `/Logout`), `Controllers/Users/` (`/Profile`, `/Settings`), and dependency injection (`Extensions/`). Everything else is still planned.
> Requirements, data model, and open questions live in [`overview_documentation.md`](overview_documentation.md). Question IDs (for example Q-BOOK-3) refer to the Open Questions section there.
> How code is written (comments, naming, spacing) is in [`coding_style_documentation.md`](coding_style_documentation.md).
> How to run the database and try the pages is in [`local_testing_documentation.md`](local_testing_documentation.md).

---

The layer order follows Section 3.1 of the overview: **Controller -> Service -> Repository -> EF Core (DbContext) -> PostgreSQL**, with Razor views rendered by controllers.

## 1. Conventions

- **Namespaces start with `StartupLab`** and follow the folder (`StartupLab.Services.Users`, `StartupLab.Models.Entities`). Don't use `Startup.Lab`: a root namespace named `Startup` hides the `Startup` entity class (error CS0118).
- **One class or interface per file.** The file name matches the type name (`UserService.cs` contains `UserService`).
- **One folder per group** in `Controllers/`, `Services/`, and `Repositories/`. The folder is plural when the singular name would hide an entity (`Users/`, not `User/`, same rule as `NewsItems/`).
- **Interfaces in an `Interfaces/` subfolder** of that service or repository group. Each implementation has its own interface (`IUserService` / `UserService`). Controllers have no interfaces.
- **Controllers depend only on service interfaces.** Services depend only on repository interfaces. Repositories are the only layer that uses the `DbContext`.
- **Entities (database tables) and ViewModels (page data) are separate.** Views never receive entities directly.
- **One folder per entity group in `Repositories/`.** Join tables stay with their main entity (`Startups/` also handles `UserStartup` and `StartupCategory`). Child tables that a user adds, changes and deletes on their own, such as photos, contacts and links, get their own group (`UserPhotos/`, `StartupLinks/`), so each repository stays small.
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
├── Controllers/                     # one folder per area, same as Repositories/; no Interfaces/ folder
│   ├── Home/                        # / (Landing), /PP (Privacy and Policy)
│   │   └── HomeController.cs
│   ├── Account/                     # /Login, /Register, /Logout
│   │   └── AccountController.cs
│   ├── Users/                       # /Profile, /Settings (not "User/")
│   │   ├── CurrentUser.cs           # signed-in user id from the authentication cookie
│   │   ├── ProfileController.cs
│   │   └── SettingsController.cs
│   ├── Startups/                    # /Startups, /Startups/{id} (planned)
│   ├── Founders/                    # /Founders, /Founders/{id} (planned)
│   ├── News/                        # /News (planned; folder name is the page, the entity stays NewsItems in repositories)
│   ├── AboutUs/                     # /AboutUs, /AboutUs/Team (planned)
│   ├── MyStartups/                  # /MyStartups (planned)
│   ├── Booking/                     # /Booking; mentor booking page (pending: Q-BOOK-14)
│   ├── Admin/                       # give mentor role (pending: route, Q-ROLE-4)
│   ├── Archive/                     # /Archive (planned)
│   └── Toolkit/                     # /Toolkit and its sub-pages (planned)
│
├── Models/
│   ├── Entities/                    # one class per database table (Section 6)
│   │   ├── User.cs                  # includes IsMentor, IsInvestor, IsAdmin (no IsFounder)
│   │   ├── UserPhoto.cs
│   │   ├── UserContact.cs
│   │   ├── UserExperience.cs
│   │   ├── Startup.cs
│   │   ├── StartupPhoto.cs
│   │   ├── StartupVideo.cs
│   │   ├── StartupDocument.cs
│   │   ├── StartupLink.cs
│   │   ├── StartupChecklist.cs      # one shared list for every startup
│   │   ├── StartupChecklistProgress.cs  # join table (startup progress)
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
│   │   ├── ToolkitChecklistProgress.cs  # join table (user progress)
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
│       │   ├── StartupFormViewModel.cs      # create/edit from My Startups (pending: Q-PR-2)
│       │   └── StartupChecklistViewModel.cs # startup checklist with this startup's progress (page pending: Q-SU-16)
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
│       │   ├── ToolkitDocumentsViewModel.cs
│       │   ├── ToolkitGlossaryViewModel.cs
│       │   └── ToolkitChecklistViewModel.cs
│       ├── Profile/
│       │   └── ProfileViewModel.cs
│       ├── Settings/
│       │   └── SettingsViewModel.cs
│       └── Shared/
│           └── PagedResultViewModel.cs      # pagination (Section 3.3 "Pagination and limits")
│
├── Services/                        # one folder per area, same as Repositories/; Interfaces/ inside the group
│   ├── Users/                       # profile, photo, contacts, experience, deactivate and hard delete (not "User/")
│   │   ├── Interfaces/
│   │   │   └── IUserService.cs
│   │   ├── PhotoChange.cs           # result of setting a photo: saved or not, and the previous address key
│   │   └── UserService.cs
│   ├── Account/                     # register, login, logout. Forgot password is still open (Q-AUTH-7)
│   │   ├── Interfaces/
│   │   │   └── IAccountService.cs
│   │   ├── AccountResult.cs         # success, or messages safe to show on the form
│   │   └── AccountService.cs
│   ├── Startups/                    # (planned)
│   ├── Founders/                    # (planned)
│   ├── NewsItems/                   # (planned; not "News/", same reason as the repository group)
│   ├── Mentors/                     # (planned)
│   ├── Admin/                       # (planned, Q-ROLE-4)
│   ├── Categories/                  # categories and tags (planned)
│   ├── Toolkit/                     # videos, documents, glossary (planned)
│   ├── ToolkitChecklists/           # (planned)
│   ├── StartupChecklists/           # (planned)
│   ├── Bookings/                    # (pending: Q-BOOK-3)
│   ├── Archive/                     # (planned)
│   └── FileStorage/                 # (pending: Q-DATA-2, what address_key is)
│
├── Repositories/                    # one folder per entity group; every group has the same four files as Users/
│   ├── Users/                       # User, UserExperience (mentors are users with IsMentor); soft (DeactivateAsync) and hard (DeleteAsync) delete; both deactivate startups left with no active founders
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
│   ├── UserContacts/                # UserContact (a user can have many; every change is checked against the owner)
│   │   ├── Interfaces/
│   │   │   ├── IUserContactReadRepository.cs
│   │   │   └── IUserContactWriteRepository.cs
│   │   ├── UserContactReadRepository.cs
│   │   └── UserContactWriteRepository.cs
│   ├── Startups/                    # Startup, UserStartup (founders), StartupCategory; list search and filters, IsFounderAsync for permission checks, soft and hard delete
│   │   ├── Interfaces/
│   │   │   ├── IStartupReadRepository.cs
│   │   │   └── IStartupWriteRepository.cs
│   │   ├── StartupFounderRemoval.cs # shared by Startups/ and Users/: a startup left with no active founders becomes inactive (overview §7.4)
│   │   ├── StartupReadRepository.cs
│   │   └── StartupWriteRepository.cs
│   ├── StartupPhotos/               # StartupPhoto (many per startup, at most one primary; delete returns the address_key)
│   │   ├── Interfaces/
│   │   │   ├── IStartupPhotoReadRepository.cs
│   │   │   └── IStartupPhotoWriteRepository.cs
│   │   ├── StartupPhotoReadRepository.cs
│   │   └── StartupPhotoWriteRepository.cs
│   ├── StartupVideos/               # StartupVideo (delete returns the address_key)
│   │   ├── Interfaces/
│   │   │   ├── IStartupVideoReadRepository.cs
│   │   │   └── IStartupVideoWriteRepository.cs
│   │   ├── StartupVideoReadRepository.cs
│   │   └── StartupVideoWriteRepository.cs
│   ├── StartupDocuments/            # StartupDocument (delete returns the address_key)
│   │   ├── Interfaces/
│   │   │   ├── IStartupDocumentReadRepository.cs
│   │   │   └── IStartupDocumentWriteRepository.cs
│   │   ├── StartupDocumentReadRepository.cs
│   │   └── StartupDocumentWriteRepository.cs
│   ├── StartupLinks/                # StartupLink (URLs, no stored file)
│   │   ├── Interfaces/
│   │   │   ├── IStartupLinkReadRepository.cs
│   │   │   └── IStartupLinkWriteRepository.cs
│   │   ├── StartupLinkReadRepository.cs
│   │   └── StartupLinkWriteRepository.cs
│   ├── NewsItems/                   # News, NewsTag (not "News/", see Conventions); hard delete only; SetPrimaryAsync makes one item featured
│   │   ├── Interfaces/
│   │   │   ├── INewsReadRepository.cs
│   │   │   └── INewsWriteRepository.cs
│   │   ├── NewsReadRepository.cs
│   │   └── NewsWriteRepository.cs
│   ├── NewsPhotos/                  # NewsPhoto (many per news item, at most one primary; delete returns the address_key)
│   │   ├── Interfaces/
│   │   │   ├── INewsPhotoReadRepository.cs
│   │   │   └── INewsPhotoWriteRepository.cs
│   │   ├── NewsPhotoReadRepository.cs
│   │   └── NewsPhotoWriteRepository.cs
│   ├── NewsVideos/                  # NewsVideo (many per news item, at most one primary; delete returns the address_key)
│   │   ├── Interfaces/
│   │   │   ├── INewsVideoReadRepository.cs
│   │   │   └── INewsVideoWriteRepository.cs
│   │   ├── NewsVideoReadRepository.cs
│   │   └── NewsVideoWriteRepository.cs
│   ├── Categories/                  # Category (not "Category/", see Conventions); soft delete only. Startup links stay in Startups/
│   │   ├── Interfaces/
│   │   │   ├── ICategoryReadRepository.cs
│   │   │   └── ICategoryWriteRepository.cs
│   │   ├── CategoryReadRepository.cs
│   │   └── CategoryWriteRepository.cs
│   ├── Tags/                        # Tag (not "Tag/", see Conventions); soft delete only. News links stay in NewsItems/
│   │   ├── Interfaces/
│   │   │   ├── ITagReadRepository.cs
│   │   │   └── ITagWriteRepository.cs
│   │   ├── TagReadRepository.cs
│   │   └── TagWriteRepository.cs
│   ├── ToolkitVideos/               # ToolkitVideo; hard delete only (no is_active); delete returns the address_key
│   │   ├── Interfaces/
│   │   │   ├── IToolkitVideoReadRepository.cs
│   │   │   └── IToolkitVideoWriteRepository.cs
│   │   ├── ToolkitVideoReadRepository.cs
│   │   └── ToolkitVideoWriteRepository.cs
│   ├── ToolkitDocuments/            # ToolkitDocument (links, no stored file); hard delete only (no is_active)
│   │   ├── Interfaces/
│   │   │   ├── IToolkitDocumentReadRepository.cs
│   │   │   └── IToolkitDocumentWriteRepository.cs
│   │   ├── ToolkitDocumentReadRepository.cs
│   │   └── ToolkitDocumentWriteRepository.cs
│   ├── ToolkitGlossaries/           # ToolkitGlossary (term and definition); hard delete only (no is_active)
│   │   ├── Interfaces/
│   │   │   ├── IToolkitGlossaryReadRepository.cs
│   │   │   └── IToolkitGlossaryWriteRepository.cs
│   │   ├── ToolkitGlossaryReadRepository.cs
│   │   └── ToolkitGlossaryWriteRepository.cs
│   ├── ToolkitChecklists/           # ToolkitChecklist, ToolkitChecklistProgress (per user; a missing progress row means not done)
│   │   ├── Interfaces/
│   │   │   ├── IToolkitChecklistReadRepository.cs
│   │   │   └── IToolkitChecklistWriteRepository.cs
│   │   ├── ToolkitChecklistReadRepository.cs
│   │   └── ToolkitChecklistWriteRepository.cs
│   ├── StartupChecklists/           # StartupChecklist, StartupChecklistProgress (per startup; a missing progress row means not done)
│   │   ├── Interfaces/
│   │   │   ├── IStartupChecklistReadRepository.cs
│   │   │   └── IStartupChecklistWriteRepository.cs
│   │   ├── StartupChecklistReadRepository.cs
│   │   └── StartupChecklistWriteRepository.cs
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
│   ├── Shared/                      # _Layout (Sidebar, Footer), Error, partials
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
│   └── Toolkit/                     # Index, Videos, Pathway, Checklist, Documents, Glossary
│
└── wwwroot/                         # css, js, images, lib
```

## 3. What each service is responsible for

| Service | Responsibility | Uses repositories |
|---|---|---|
| `AccountService` | Register, login, and logout. Lives in `Services/Account/`. Uses ASP.NET Core Identity (`UserManager`, `SignInManager`) because the password is not a column on `User`. Email must be unique. An inactive user cannot sign in. Forgot password is still open (Q-AUTH-7). Email confirmation is not required (Q-AUTH-6) | Identity, not a repository |
| `UserService` | Profile data, photos, contacts, experience, and deactivating or deleting the account. Lives in `Services/Users/`. `Controllers/Users/ProfileController` (`/Profile`) and `SettingsController` (`/Settings`) are the callers. The signed-in user comes from `CurrentUser`, which reads the id set by `AccountService`. Password and email changes stay open (Q-PR-3) | `IUserReadRepository`, `IUserWriteRepository`, `IUserPhotoWriteRepository`, `IUserContactWriteRepository` |
| `StartupService` | Startup list (search, filters, pagination), details, create/edit, founders and categories, photos/videos/documents/links, the user's own startups for My Startups. Only the startup's founders (`IsFounderAsync`) and admins may change or delete it (overview §7.4) | `IStartupReadRepository`, `IStartupWriteRepository`, `IStartupPhotoReadRepository`, `IStartupPhotoWriteRepository`, `IStartupVideoReadRepository`, `IStartupVideoWriteRepository`, `IStartupDocumentReadRepository`, `IStartupDocumentWriteRepository`, `IStartupLinkReadRepository`, `IStartupLinkWriteRepository`, `ICategoryReadRepository` |
| `FounderService` | Founder list and details (every user is a founder) | `IUserReadRepository`, `IStartupReadRepository` |
| `NewsService` | News list (pagination, tag filter), featured news (`is_primary`), create/edit/delete, photos and videos. News is only hard deleted (no `is_active`) | `INewsReadRepository`, `INewsWriteRepository`, `INewsPhotoReadRepository`, `INewsPhotoWriteRepository`, `INewsVideoReadRepository`, `INewsVideoWriteRepository`, `ITagReadRepository` |
| `MentorService` | Mentors (users with `IsMentor`) for About Us, Landing "Meet the Crew", and the booking page | `IUserReadRepository` |
| `AdminService` | Give the mentor or investor role to any user (only admins). Other admin actions are pending (Q-ROLE-4) | `IUserReadRepository`, `IUserWriteRepository` |
| `CategoryService` | Categories (Startups) and tags (News): lists for filters, and create, edit and soft delete. Assigning them to a startup or a news item stays in those repositories | `ICategoryReadRepository`, `ICategoryWriteRepository`, `ITagReadRepository`, `ITagWriteRepository` |
| `ToolkitService` | Toolkit videos, documents (links), glossary, pathways; create, edit and delete for videos, documents and glossary entries. They have no `is_active`, so they are only hard deleted | `IToolkitVideoReadRepository`, `IToolkitVideoWriteRepository`, `IToolkitDocumentReadRepository`, `IToolkitDocumentWriteRepository`, `IToolkitGlossaryReadRepository`, `IToolkitGlossaryWriteRepository` |
| `ToolkitChecklistService` | Founder checklist in the Toolkit and each user's progress | `IToolkitChecklistReadRepository`, `IToolkitChecklistWriteRepository` |
| `StartupChecklistService` | Startup checklist and each startup's progress (only the startup's founders can mark it, Q-SU-16) | `IStartupChecklistReadRepository`, `IStartupChecklistWriteRepository`, `IStartupReadRepository` |
| `BookingService` | Users booking mentors, mentor availability, mentor booking page (pending: Q-BOOK-14) | `IBookingReadRepository`, `IBookingWriteRepository`, `IUserReadRepository` |
| `ArchiveService` | Inactive startups and founders | `IStartupReadRepository`, `IUserReadRepository` |
| `FileStorageService` | Saving/reading files referenced by `address_key` | none (file storage, pending Q-DATA-2) |

## 4. Not in the structure yet

- **Investors:** `User.IsInvestor` exists (with `SetInvestorAsync` / `GetInvestorsAsync` in `Repositories/Users/`). No other investor files until Q-INV-1 is answered.
- **Highlighted startups:** skipped for now. Later a `HighlightedStartup` entity (table `highlighted_startup`, a list of startup ids) and its repository group will be added for the Landing page (overview §6.9, Q-SU-9).
- **Admin area:** only "give mentor role" and "edit or delete any startup" are decided (`AdminController` / `AdminService` above). Other admin pages and the admin route are not defined (Q-ROLE-4).
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
| 0.8 | 2026-10-28 | Added the `Repositories/UserContacts/` group; contact methods moved out of `Users/`. | Aleksandre Nadirashvili |
| 0.9 | 2026-11-05 | Followed overview v0.8. `UserChecklist` renamed to `ToolkitChecklistProgress`. Added `IsInvestor`, the `StartupChecklist` and `StartupChecklistProgress` entities, and the `Repositories/ToolkitChecklists/` and `Repositories/StartupChecklists/` groups (one per checklist, named after its tables). The planned checklist service and view model are split the same way. Added the Toolkit Checklist, Documents and Glossary pages (planned). `_Layout` uses a sidebar instead of the header. | Aleksandre Nadirashvili |
| 0.10 | 2026-11-08 | Added the `Repositories/Startups/` group (startups, founders, categories) and separate `StartupPhotos/`, `StartupVideos/`, `StartupDocuments/` and `StartupLinks/` groups, following `UserPhotos/` and `UserContacts/`. Conventions explain when a child table gets its own group. `StartupService` lists the new repositories. Followed overview v0.9: users and startups have both soft delete (`DeactivateAsync` / `ActivateAsync`) and hard delete (`DeleteAsync`); which one the services use is Q-DATA-9. Checklist items are only soft deleted, tables without `is_active` only hard deleted. Removing a founder or deleting a user makes startups left with no active founders inactive (`StartupFounderRemoval`). Highlighted startups are planned for later. | Aleksandre Nadirashvili |
| 0.11 | 2026-11-12 | Added the `Repositories/NewsItems/` group (news items and their tags). News has no `is_active`, so it is only hard deleted. `SetPrimaryAsync` makes one news item the featured item. | Aleksandre Nadirashvili |
| 0.12 | 2026-11-12 | Added the `Repositories/NewsPhotos/` group. A news item can have many photos, and at most one is primary. | Aleksandre Nadirashvili |
| 0.13 | 2026-11-12 | Added the `Repositories/NewsVideos/` group. A news item can have many videos, and at most one is primary. | Aleksandre Nadirashvili |
| 0.14 | 2026-11-13 | Added the `Repositories/Categories/` group. The folder is plural so it does not hide the `Category` entity. Categories are only soft deleted. | Aleksandre Nadirashvili |
| 0.15 | 2026-11-13 | Added the `Repositories/Tags/` group. The folder is plural so it does not hide the `Tag` entity. Tags are only soft deleted. Assigning a tag to a news item stays in `NewsItems/`. | Aleksandre Nadirashvili |
| 0.16 | 2026-11-16 | Added the `Repositories/ToolkitVideos/`, `Repositories/ToolkitDocuments/` and `Repositories/ToolkitGlossaries/` groups instead of one planned `Toolkit/` group, one per page like the News groups. None of the three tables has `is_active`, so they are only hard deleted; deleting a video returns its `address_key`. `ToolkitService` lists the new repositories. | Nikoloz Khidasheli |
| 0.17 | 2026-11-21 | Added `UserService` with `ProfileController` (`/Profile`) and `SettingsController` (`/Settings`). Profile edits name, photo, contacts and experience. Settings deactivates or hard-deletes the account. Both require a signed-in user id, which login does not set yet. | Aleksandre Nadirashvili |
| 0.18 | 2026-11-21 | Controllers and services use one folder per area, the same way repositories do. Built groups are `Controllers/Home/`, `Controllers/Account/`, `Controllers/Users/`, and `Services/Users/` with its own `Interfaces/` folder. A group folder is plural when the singular name would hide an entity. | Aleksandre Nadirashvili |
| 0.19 | 2026-11-21 | Added `Services/Account/` and connected `/Register`, `/Login`, and `/Logout`. Identity stores the password and sets the sign-in cookie that `CurrentUser` reads. A new account is signed in and sent to `/Profile`. | Aleksandre Nadirashvili |
| 0.20 | 2026-11-21 | Linked the local testing guide, which covers the PostgreSQL container, migrations, and trying the pages. | Aleksandre Nadirashvili |
