# Founder Pathway — Backend Implementation Plan

> **Status:** Draft v0.1. Backend only: entities, EF Core, repository, service, controller, dependency injection, tests. Views are mentioned only where the controller needs them.
> This document explains **what** to build and **why**, step by step. It contains no code; each step describes the classes, fields, methods, and rules so whoever implements it can write the code.
> Related: [`overview_documentation.md`](overview_documentation.md) (requirements, open questions), [`structure_documentation.md`](structure_documentation.md) (folders, conventions), [`development_workflow_documentation.md`](development_workflow_documentation.md) (pre-push checks).

---

## 0. What we are building and what is assumed

**Source in the Excel:** the Founders Toolkit page has a sub-navigation item and a content section called **Founder Pathways**. The Pages sheet lists "Founder Pathways Page" under the Toolkit, and Routes has **`/Toolkit/Pathway`**. **No table exists** for it (overview §6.9, Q-TK-1, Q-TK-2).

Because there is no table, this plan **proposes** a minimal design. Every assumption is marked **A-n** and listed again in [Section 14](#14-assumptions-and-questions-to-confirm) so it can be confirmed or changed before anyone writes code.

| # | Assumption |
|---|---|
| A-1 | A **pathway** is an ordered guide (for example "From idea to MVP") made of ordered **steps**. Each step has a title and text content. |
| A-2 | There can be **several pathways**. `/Toolkit/Pathway` lists them, and `/Toolkit/Pathway/{id}` shows one pathway with its steps. (Routes only lists `/Toolkit/Pathway`.) |
| A-3 | Reading pathways is **public**. The Toolkit is labelled "public/user required" (Q-ROLE-6). |
| A-4 | Only **admins** create and edit pathways (Q-TK-9, Q-ROLE-4). That is Phase 2; Phase 1 is read-only. |
| A-5 | Tables follow the style of the Tables sheet: snake_case names, `int` primary keys, `display_order`, `is_active`, `date_created`, nullable `date_modified` (Q-DATA-7, Q-DATA-8). |
| A-6 | Per-user progress on pathways is **not** stored. Progress tracking already exists through the Checklist (`User_Checklist`). |

### Where pathways fit in the structure

Following `structure_documentation.md` §1 ("one repository per main entity group"), pathways belong to the **Toolkit** group. They are added to the planned `ToolkitRepository`, `ToolkitService`, and `ToolkitController`, not to new pathway-only classes.

### Files involved

| File | New or edited | Step |
|---|---|---|
| `MentorBooking.csproj` | edited (packages) | 1 |
| `appsettings.json` | edited (connection string key) | 1 |
| `Program.cs` | edited (database and DI registration) | 1, 9 |
| `Data/ApplicationDbContext.cs` | new (first database feature) or edited | 1, 4 |
| `Models/Entities/ToolkitPathway.cs` | new | 2 |
| `Models/Entities/ToolkitPathwayStep.cs` | new | 2 |
| `Data/Configurations/ToolkitPathwayConfiguration.cs` | new | 3 |
| `Data/Configurations/ToolkitPathwayStepConfiguration.cs` | new | 3 |
| `Data/Migrations/…AddToolkitPathway…` | generated | 5 |
| `Repositories/Interfaces/IToolkitRepository.cs` and `Repositories/ToolkitRepository.cs` | new or edited | 6 |
| Four view models in `Models/ViewModels/Toolkit/` | new | 7 |
| `Services/Interfaces/IToolkitService.cs` and `Services/ToolkitService.cs` | new or edited | 8 |
| `Extensions/ServiceCollectionExtensions.cs` | new (first feature) or edited | 9 |
| `Controllers/ToolkitController.cs` | new or edited | 10 |

---

## 1. One-time prerequisites (skip anything already done)

Entity Framework Core isn't in the project yet. Whoever builds the first database feature does this once, and every later feature reuses it. As soon as EF Core appears in the csproj, the `EF migrations` CI check turns on automatically.

**1.1 Packages.** Add EF Core, the EF Core design-time package (needed for migrations), the Npgsql PostgreSQL provider, and the `EFCore.NamingConventions` package (for snake_case names, see 1.5). Their major version must match the target framework: the project targets .NET 10, so use version 10.x (see Q-TECH-1 about .NET 9 vs 10). Also install the `dotnet-ef` command-line tool. Afterwards, run the vulnerable-package check from the workflow doc, because new packages bring new transitive dependencies.

**1.2 Local database.** Every developer needs a local PostgreSQL. The simplest option is a PostgreSQL Docker container on port 5432.

**1.3 Connection string.** The real connection string must **never** be committed (overview §3.3, "Secrets management"). `appsettings.json` only gets an empty `ConnectionStrings:DefaultConnection` entry, so the expected key is documented. Each developer stores the real value with .NET user secrets on their own machine. Production gets it from environment variables or the host's secret store (Q-TECH-2).

**1.4 DbContext.** Create `ApplicationDbContext` in `Data/`. It inherits from EF Core's `DbContext`. When the model is built, it automatically loads every configuration class in `Data/Configurations/`. That way each new entity only needs its own configuration file, not edits to the context.

**1.5 Register the database in `Program.cs`.** Register `ApplicationDbContext` with the Npgsql provider and the `DefaultConnection` connection string, and turn on the snake_case naming convention. The convention maps C# names like `ToolkitPathwayId` to `toolkit_pathway_id` automatically, so columns don't have to be renamed one by one. **This is a project-wide decision (Q-DATA-8).** Agree on it before the first migration, because changing it later renames every table.

---

## 2. Entities — `Models/Entities/`

Entities represent database tables only. They are never passed to a view (structure §1).

### 2.1 `ToolkitPathway` → table `toolkit_pathway`

| Property | Type | Required | Meaning |
|---|---|---|---|
| `ToolkitPathwayId` | int | yes (PK) | Primary key, auto-increment |
| `Title` | text | yes | Name of the pathway, e.g. "From idea to MVP" |
| `Description` | text | no | Short summary shown on the list page |
| `DisplayOrder` | int | yes | Position on the list page (lower first) |
| `IsActive` | bool | yes | Hidden from the public when false (soft delete) |
| `DateCreated` | date-time | yes | When it was created |
| `DateModified` | date-time | no | When it was last edited |
| `Steps` | collection of `ToolkitPathwayStep` | | Navigation property to its steps |

### 2.2 `ToolkitPathwayStep` → table `toolkit_pathway_step`

| Property | Type | Required | Meaning |
|---|---|---|---|
| `ToolkitPathwayStepId` | int | yes (PK) | Primary key, auto-increment |
| `ToolkitPathwayId` | int | yes (FK) | The pathway this step belongs to |
| `Title` | text | yes | Step name, e.g. "Talk to 10 users" |
| `Content` | text | yes | The explanation of the step |
| `DisplayOrder` | int | yes | Position inside the pathway |
| `DateCreated` | date-time | yes | When it was created |
| `DateModified` | date-time | no | When it was last edited |
| `Pathway` | `ToolkitPathway` | | Navigation property back to the parent |

### Rules for entities

- **All dates are stored in UTC.** The Npgsql provider rejects saving a date-time that isn't UTC, so always use the current UTC time, never local time.
- Text and string properties are non-nullable unless the table above says "no". This matches the project's nullable reference types setting.
- Steps have no `IsActive`. Removing a step means deleting it (see Q-DATA-5, soft vs hard delete).

---

## 3. EF Core configuration — `Data/Configurations/`

One configuration class per entity, using the EF Core Fluent API. The database enforces constraints too, not only the C# code (overview §3.3, "Database constraints").

### 3.1 `ToolkitPathwayConfiguration`

- Primary key: `ToolkitPathwayId`.
- `Title` is required, max **200** characters.
- `Description` is optional, max **2000** characters.
- `IsActive` defaults to **true** in the database.
- `DateCreated` defaults to the **database's current time**, so a row inserted by hand (dev data) still gets a date.
- **Relationship:** one pathway has many steps through the `ToolkitPathwayId` foreign key, with **cascade delete**. Deleting a pathway row deletes its steps. In normal use pathways are deactivated, not deleted.
- **Index** on (`IsActive`, `DisplayOrder`). It matches the list query: "active pathways, ordered by display order".

### 3.2 `ToolkitPathwayStepConfiguration`

- Primary key: `ToolkitPathwayStepId`.
- `Title` is required, max **200** characters.
- `Content` is required, with no length limit (stored as PostgreSQL `text`).
- `DateCreated` defaults to the database's current time.
- **Index** on (`ToolkitPathwayId`, `DisplayOrder`). It matches the details query: "steps of this pathway, in order".
- `DisplayOrder` is deliberately **not unique**. A unique constraint makes swapping two steps fail halfway through a reorder. When two steps have the same order, the queries break the tie by id.

---

## 4. Register the tables in `ApplicationDbContext`

Add two `DbSet` properties to the context, one for pathways and one for steps. These are what the repository queries.

---

## 5. Migration

1. Create a migration named **`AddToolkitPathway`**, with its files written to `Data/Migrations/` (the folder from the structure doc).
2. Apply it to your local database.
3. Run the "has pending model changes" check. It must report no changes. This is exactly what the `EF migrations` CI check runs.

**Check the generated migration before committing:**
- the two tables `toolkit_pathway` and `toolkit_pathway_step` exist with snake_case columns
- the foreign key from step to pathway has cascade delete
- both indexes from Section 3 are there
- the title lengths are 200 and the defaults (current time, `true`) are set

Commit the entities, configurations, and migration files **together** (workflow doc §2.4). Never edit a migration that is already on `main`; add a new one instead.

---

## 6. Repository — `Repositories/`

The repository is the **only** layer that talks to `ApplicationDbContext` (structure §1). It receives the context through constructor injection.

### 6.1 `IToolkitRepository` — methods for pathways

| Method | Input | Returns | What it does |
|---|---|---|---|
| `GetActivePathwaysAsync` | max number of rows, cancellation token | list of pathways (without steps) | Active pathways only, ordered by `DisplayOrder` then id, limited to the max number |
| `GetActivePathwayWithStepsAsync` | pathway id, cancellation token | one pathway with its steps, or nothing | The pathway with that id **if it is active**, with its steps loaded and ordered by `DisplayOrder` then id |

Methods for toolkit videos, documents, and the glossary will be added to the same interface by their own features.

### 6.2 `ToolkitRepository` — rules

- **Read-only queries use no-tracking.** EF Core then doesn't keep a copy for change tracking, which is faster and uses less memory.
- **No N+1 queries** (overview §3.2):
  - The list query doesn't load steps at all, because the list page doesn't show them.
  - The details query loads the pathway **and** its steps in **one** SQL query, using EF Core's eager loading (Include) with ordering applied to the included steps.
  - Never loop over pathways and load each one's steps separately.
- **No unbounded lists** (overview §3.3): the list query always takes a maximum count. There will be few pathways, so a cap is enough and pagination isn't needed.
- **Inactive pathways are invisible.** The details method returns nothing for an inactive pathway, and the controller turns that into a 404.
- **Check the SQL** while developing: turn on EF Core command logging in `appsettings.Development.json` (log category `Microsoft.EntityFrameworkCore.Database.Command`, level Information). Confirm each page runs exactly one query.

---

## 7. View models — `Models/ViewModels/Toolkit/`

Views receive only these classes, never entities. One class per file. The service fills them.

| View model | Used by | Fields |
|---|---|---|
| `ToolkitPathwayListViewModel` | list page | `Pathways`: list of `ToolkitPathwayListItemViewModel` |
| `ToolkitPathwayListItemViewModel` | one card on the list page | `Id`, `Title`, `Description` (optional) |
| `ToolkitPathwayViewModel` | details page | `Id`, `Title`, `Description` (optional), `Steps`: list of `ToolkitPathwayStepViewModel` |
| `ToolkitPathwayStepViewModel` | one step on the details page | `StepNumber` (1, 2, 3… shown to the user, **not** the database id), `Title`, `Content` |

The structure doc already lists `ToolkitPathwayViewModel.cs`. The other three are new and should be added to its folder tree when this is built.

---

## 8. Service — `Services/`

The service holds the business rules and converts entities into view models. It depends only on `IToolkitRepository`, never on the DbContext.

### 8.1 `IToolkitService` — methods for pathways

| Method | Input | Returns |
|---|---|---|
| `GetPathwaysAsync` | cancellation token | `ToolkitPathwayListViewModel` |
| `GetPathwayAsync` | pathway id, cancellation token | `ToolkitPathwayViewModel`, or nothing if not found / inactive |

### 8.2 `ToolkitService` — behaviour

- **`GetPathwaysAsync`:** asks the repository for active pathways with a maximum of **50** (a constant in the service). It maps each pathway to a list item (id, title, description). If there are no pathways, it returns an empty list, not nothing, so the view can show an "empty" message.
- **`GetPathwayAsync`:** asks the repository for the pathway with its steps.
  - If the repository returns nothing, the service returns nothing.
  - Otherwise it maps the pathway, and each step gets a `StepNumber` from its position (first step = 1).
  - The service must **keep the repository's order** and not re-sort the steps.

---

## 9. Dependency injection

Create `Extensions/ServiceCollectionExtensions.cs` once (structure §1). It has one extension method, `AddApplicationServices`, that registers every repository and service pair. For this feature, register `IToolkitRepository` → `ToolkitRepository` and `IToolkitService` → `ToolkitService`. In `Program.cs`, call `AddApplicationServices` after the database registration from step 1.5.

Register both with a **scoped** lifetime (one instance per HTTP request). The DbContext is scoped, so a longer-lived service holding it would break. In Development, ASP.NET Core reports that mistake at startup.

---

## 10. Controller — `Controllers/ToolkitController.cs`

The controller only receives the request, calls `IToolkitService`, and picks the response. It doesn't query data or map entities.

| Action | HTTP | Route | Behaviour |
|---|---|---|---|
| `Pathway` | GET | `/Toolkit/Pathway` | Gets the list view model from the service and returns the `Pathway` view |
| `PathwayDetails` | GET | `/Toolkit/Pathway/{id}` | Gets the pathway from the service. Returns **404 Not Found** if there's nothing, otherwise the `PathwayDetails` view |

Rules:
- Use **attribute routing** so the URLs match the Routes sheet exactly. Constrain `id` to integers, so `/Toolkit/Pathway/abc` is rejected before the action runs.
- Pass the request's **cancellation token** through to the service and repository. A closed browser tab then stops the database query.
- **Access (A-3):** no login is required. If Q-ROLE-6 decides pathways need login, mark these two actions as requiring authorization. That depends on authentication existing first (Q-TECH-3).
- `/Toolkit` and `/Toolkit/Videos` will be added to this same controller by their own features.

**Handover to the front end:** two views are needed, `Views/Toolkit/Pathway.cshtml` (typed to the list view model) and `Views/Toolkit/PathwayDetails.cshtml` (typed to the details view model). Step content must be rendered with Razor's normal output, which HTML-encodes it, and **never** as raw HTML (XSS risk, overview §3.3 "Output encoding"). If formatted content is wanted, see Q-PW-4.

---

## 11. Development data

Until the admin features exist (Phase 2), developers add a few pathways and steps straight into their local database, for example with a short SQL script or a database GUI. Because `DateCreated` and `IsActive` have database defaults, only title, description, content, and display order need values.

Don't put sample data inside migrations (EF Core "seed data"). Migrations also run in production, so the samples would end up on the live site.

---

## 12. Tests

There is no test project yet (structure §4, Q-TECH-7). When `MentorBooking.Tests` exists, add these tests.

**Service unit tests** (with a fake repository):
| Scenario | Expected result |
|---|---|
| Repository returns two pathways | List view model has two items with matching id, title, description |
| Repository returns no pathways | Empty list, not nothing |
| Repository returns nothing for an id | Service returns nothing |
| Pathway has three steps | Step numbers are 1, 2, 3 in the repository's order |
| List is requested | Repository is asked for at most 50 |

**Repository integration tests** (against a real PostgreSQL, e.g. with Testcontainers; EF Core's in-memory provider doesn't behave like PostgreSQL):
| Scenario | Expected result |
|---|---|
| An inactive pathway exists | It is missing from the list, and its details return nothing |
| Steps inserted in random order | Returned sorted by display order, then id |
| Details query runs | Exactly **one** SQL command is executed (protects against N+1) |

**Controller tests:**
| Scenario | Expected result |
|---|---|
| Service returns nothing | 404 Not Found |
| Service returns a pathway | View result with that model |

---

## 13. Done checklist

- [ ] Prerequisites from Section 1 exist (packages, user-secret connection string, DbContext, database registration)
- [ ] `ToolkitPathway` and `ToolkitPathwayStep` entities
- [ ] Both configuration classes, with constraints and indexes
- [ ] DbSets added to the context
- [ ] Migration `AddToolkitPathway` created, reviewed, and applied locally
- [ ] `IToolkitRepository` / `ToolkitRepository` with the two read methods
- [ ] Four view models
- [ ] `IToolkitService` / `ToolkitService`
- [ ] Both registered in `AddApplicationServices`, which is called from `Program.cs`
- [ ] `ToolkitController` with the two actions
- [ ] Manual check: list page works, details page works, unknown id gives 404, inactive pathway gives 404
- [ ] All pre-push checks pass (workflow doc §2.6), including the pending-model-changes check

---

## Phase 2 — Admin management (after Q-TK-9 and Q-ROLE-4 are answered)

Start this only once authentication and an **Admin** role exist (Q-TECH-3).

**Repository additions:**
- Load a pathway for editing: **with** tracking, with its steps, active or not, since admins must also see inactive pathways.
- Add a new pathway, remove a step, and save changes.

**Service additions:**
| Operation | Rules |
|---|---|
| Create pathway | Sets the creation date. New pathways go to the end of the list (highest display order + 1). Returns the new id. |
| Update pathway | Changes title/description. Sets `DateModified` to the current UTC time. Returns "not found" if the id doesn't exist. |
| Activate / deactivate pathway | The soft delete. Deactivated pathways disappear from the public pages. |
| Add step | Appends to the end of the pathway (highest display order + 1). |
| Update step | Changes title/content and sets `DateModified`. |
| Delete step | Hard delete. |
| Reorder steps | Receives the step ids in their new order. **Rejects** the request unless the ids are exactly the pathway's steps (none missing, none extra). Then rewrites the display orders as 1, 2, 3… |

Every "not found" result makes the controller return 404.

**Form view models:** `ToolkitPathwayFormViewModel` and `ToolkitPathwayStepFormViewModel`. Their validation attributes (required fields, max 200 characters for titles) must match the database limits from Section 3.

**Admin controller:**
- A separate controller. Its location depends on the admin-area decision (Q-ROLE-4).
- Restricted to the **Admin** role at class level.
- Every POST action validates the **antiforgery token** (CSRF, overview §3.3).
- Every POST action checks **model validation first**, returning the form with errors when invalid, before calling the service (server-side validation, overview §3.3).

---

## 14. Assumptions and questions to confirm

Answer format: "A-2: …" or "Q-PW-1: …".

- **A-1 … A-6:** see Section 0.
- **Q-PW-1:** One pathway or several? If there is only one, drop the list page and let `/Toolkit/Pathway` show the steps directly.
- **Q-PW-2:** Does a step need more than a title and text, such as links to a Toolkit Video or Document, an estimated duration, or a link to a checklist item?
- **Q-PW-3:** Should founders be able to mark steps as done (a new `user_pathway_step` table, like `User_Checklist`), or is the Checklist enough (A-6)?
- **Q-PW-4:** Is step content plain text, Markdown, or rich HTML? Markdown or HTML must be sanitized before it is shown.
- **Q-PW-5:** Is "Requirements" (Toolkit section, Q-TK-2) part of the pathway, for example as a type of step, or a separate feature?

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-20 | First version: backend plan for Founder Pathways (read-only Phase 1, admin outline Phase 2). | Nikoloz Khidasheli |
