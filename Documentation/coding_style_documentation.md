# Coding Style Documentation

> **Status:** Draft v0.1.
> How C# code in this project is written: comments, naming, spacing, and the patterns used in entities and repositories. The rules come from the code that already exists, so new code should look the same.
> Related: [`structure_documentation.md`](structure_documentation.md) (folders, layers), [`development_workflow_documentation.md`](development_workflow_documentation.md) (pre-push checks, including `dotnet format`).

---

## 1. Comments

### 1.1 Keep them simple

- Write **short, plain sentences**. One or two sentences is usually enough.
- Explain **what** something is or does, in words a new team member understands. Don't repeat the code.
- No jargon you wouldn't say out loud, and no long explanations. If it needs a paragraph, it belongs in the documentation, not in the code.

| Good | Too complicated |
|---|---|
| `Gets the user's photo, or null if they have none.` | `Asynchronously retrieves the UserPhoto entity associated with the specified user identifier using a no-tracking query, returning null when no matching record exists.` |

### 1.2 Where to put `/// <summary>`

| Where | Summary needed? |
|---|---|
| Every class and interface (entities, repositories, extensions) | Yes |
| Every method on a repository or service **interface** | Yes |
| Methods on the **implementation** class | No. The interface already describes them. The class summary points to the interface: `EF Core implementation of <see cref="IUserReadRepository"/>.` |
| Entity properties | Only when the name doesn't explain it (for example `AddressKey`, `DisplayOrder`, `IsPrimary`, `IsMentor`). Not for `Id`, `Name`, `Title`, `DateCreated`, foreign keys, or navigation properties. |

### 1.3 How to write a summary

- Methods start with a verb: **Gets**, **Adds**, **Changes**, **Deletes**, **Counts**.
- Always say what happens when nothing is found: "or null if not found", "Returns false if the user doesn't exist".
- Use `<returns>` only when the return value needs its own explanation (for example the old `AddressKey` returned by `IUserPhotoWriteRepository.SetByUserIdAsync`).
- Use `<see cref="..."/>` to point to another type or method instead of repeating its explanation.
- Repeated properties get the **same** sentence everywhere. For example, every `DisplayOrder` says "Position in the list. Lower numbers come first."

Format: three lines, even for one sentence.

```csharp
/// <summary>
/// Gets a user by id, or null if not found.
/// </summary>
Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);
```

### 1.4 `//` comments inside code

- Only for something the code **can't show**, such as a reason or a rule from outside the code. Example from `UserWriteRepository`: `// ConcurrencyStamp is refreshed because Identity's UserManager uses it to detect conflicting updates.`
- Never describe what the next line does (`// save changes` above `SaveChangesAsync`).
- No commented-out code. Delete it; git keeps the history.

---

## 2. Naming

| Item | Style | Example |
|---|---|---|
| Classes, interfaces, methods, properties | PascalCase | `UserPhotoWriteRepository`, `GetByUserIdAsync`, `DateCreated` |
| Parameters and local variables | camelCase | `userId`, `addressKey`, `previousAddressKey` |
| Interfaces | `I` + name | `IUserReadRepository` |
| Async methods | end with `Async` | `SetByUserIdAsync`, `CountUsersAsync` |
| Primary key on an entity | `Id` | `UserPhoto.Id` |
| Foreign key on an entity | `{Entity}Id` | `UserPhoto.UserId` |
| Id parameters | `{entity}Id`, never just `id` | `int userId`, `int contactId` |
| Repositories | `{Group}ReadRepository` / `{Group}WriteRepository` | `UserReadRepository` |
| Boolean properties | `Is...` | `IsActive`, `IsMentor`, `IsPrimary` |

**Lambda parameters** use the first letter of the entity (`u` for User, `p` for photo, `c` for contact, `e` for experience). Join entities use two letters (`uc` for UserChecklist). Inside `ExecuteUpdateAsync`, the setter parameter is `s`:

```csharp
.Where(u => u.Id == userId)
.ExecuteUpdateAsync(s => s
    .SetProperty(u => u.IsMentor, isMentor)
    .SetProperty(u => u.DateModified, DateTime.UtcNow),
    cancellationToken) > 0;
```

---

## 3. Layout and spacing

- **Indentation:** 4 spaces, no tabs.
- **Braces:** each `{` and `}` on its own line.
- **Namespaces:** file-scoped (`namespace StartupLab.Repositories.Users;`), matching the folder.
- **One blank line between members** (properties, methods). No blank line right after `{` or right before `}`.
- **`using` directives:** at the top, `System.*` first, then the rest alphabetically (`Microsoft.*` before `StartupLab.*`). Remove unused ones. Don't add usings that implicit usings already cover (`System`, `System.Linq`, `System.Threading.Tasks`, ...).
- **Method signatures** stay on one line, even when they are long.
- **LINQ chains:** the source on the first line, then **one call per line**, indented 4 spaces more:

```csharp
public Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken) =>
    context.Users
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
```

- **`ExecuteUpdateAsync`:** each `SetProperty` on its own line (see the example in Section 2).
- **EF configurations** (`Data/Configurations/`): group related lines (table name, key, columns, relationships) and put one blank line between groups.

Run `dotnet format` before pushing. CI fails if it would change anything ([workflow doc §2.2](development_workflow_documentation.md#22-formatting-ci-check-format)).

---

## 4. Classes and methods

- **Constructor injection with primary constructors:** `public class UserReadRepository(ApplicationDbContext context) : IUserReadRepository`.
- **Expression body (`=>`)** for a method that is a single query. **Block body (`{ }`)** when there are several statements.
- **`async`/`await` only when needed.** Return the `Task` directly if nothing is done with the result (`GetByIdAsync`). Use `async`/`await` when the result is changed, for example `> 0` to turn a row count into a `bool`, or a `List` returned as `IReadOnlyList`.
- **`CancellationToken cancellationToken` is always the last parameter,** with no default value, and is passed to every EF Core call.
- **Lists are returned as `IReadOnlyList<T>`,** single rows as `T?` (null when not found).
- Use `var` when the type is clear from the right side. Use `is null` / `is not null`, not `== null`.

---

## 5. Entities (`Models/Entities/`)

- **Property order** follows the column order of the table in the overview ([Section 6](overview_documentation.md#6-data-model-sheet-tables)): `Id`, foreign keys, data columns, dates and flags in the same order as the table. **Navigation properties come last.**
- **Defaults:**
  - required strings: `= string.Empty;`
  - optional values: nullable type (`string?`, `DateTime?`), no default
  - required navigation properties: `= null!;`
  - collections: `= [];`
  - `IsActive`: `= true;`
- **All dates are UTC.** Always use `DateTime.UtcNow`, never `DateTime.Now`. PostgreSQL rejects local times.
- **No attributes** (`[Required]`, `[MaxLength]`, ...) on entities. Table names, keys, lengths and relationships go in the entity's configuration class in `Data/Configurations/`.

---

## 6. Repositories (`Repositories/`)

**Read repositories**
- Every query uses `AsNoTracking()`.
- One row by a unique key: `SingleOrDefaultAsync`.
- Lists are **never unbounded**: always `Take(...)` or paging with `skip`/`take`.
- Lists are **always ordered**, and the last sort key is `Id`, so the order is stable (`.OrderBy(u => u.Name).ThenBy(u => u.Surname).ThenBy(u => u.Id)`).
- Load related data with `Include` in the same query (no N+1). Use `AsSplitQuery()` when including several collections.

**Write repositories**
- Single update or delete: `ExecuteUpdateAsync` / `ExecuteDeleteAsync`, returning `bool` (`> 0` means a row was changed).
- Insert, or when the old value is needed: load with tracking, change it, then `SaveChangesAsync`.
- **Child rows are always filtered by their owner as well** (`c.Id == contactId && c.UserId == userId`), so one user can't change another user's data.
- When a file is replaced or deleted, return its old `AddressKey` so the caller can delete the file from storage.
- Set `DateModified = DateTime.UtcNow` on every update, if the table has that column.

---

## 7. Dependency injection

- Every interface/implementation pair is registered in `AddApplicationServices` in `Extensions/ServiceCollectionExtensions.cs`. `Program.cs` only calls that method.
- Lifetime is **scoped** (`AddScoped`), because repositories use the scoped `ApplicationDbContext`.
- One block per group, read repository first, then write repository, with a blank line between groups.

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-28 | First version: comments, naming, layout, entities, repositories, dependency injection. | Aleksandre Nadirashvili |
