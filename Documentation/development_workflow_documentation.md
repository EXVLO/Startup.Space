# Development Workflow Documentation

> **Status:** Draft v0.3.
> Commands to run before every push. They mirror the checks in `.github/workflows/build.yml`, so if they pass locally, they pass on GitHub.
> Related: [`overview_documentation.md`](overview_documentation.md), [`structure_documentation.md`](structure_documentation.md).

---

## 1. Prerequisites

| Tool | Version | Check with |
|---|---|---|
| .NET SDK | 10.x (same as CI) | `dotnet --version` |
| `dotnet-ef` (only after Entity Framework is added) | matching EF Core version | `dotnet ef --version` |

```bash
dotnet tool install --global dotnet-ef
```

All commands are run from the project root (the folder with `MentorBooking.csproj`).

---

## 2. Pre-Push Checks

### 2.1 Restore and build (CI check: `Build`)

```bash
dotnet restore
dotnet build --no-restore --configuration Release
```

Must end with `Build succeeded`. Fix warnings as well as errors; they often point to real bugs such as possible `null` values.

### 2.2 Formatting (CI check: `Format`)

```bash
dotnet format
dotnet format --verify-no-changes
```

The first command fixes formatting. The second is exactly what CI runs: no output means it passed. Commit any files the first command changed.

### 2.3 Vulnerable packages (CI check: `Vulnerable packages`)

```bash
dotnet list package --vulnerable --include-transitive
```

Passing output: `The given project 'MentorBooking' has no vulnerable packages given the current sources.`

If a package is listed:

```bash
dotnet add package <PackageName> --version <safe-version>
```

For transitive (indirect) packages, update the direct package that depends on it, or pin the safe version with `dotnet add package`.

### 2.4 Entity Framework migrations (CI check: `EF migrations`)

Applies once Entity Framework Core is installed. Until then, CI skips this check.

After changing anything in `Models/Entities/` or `Data/`:

```bash
dotnet ef migrations add <DescriptiveName>
dotnet ef migrations has-pending-model-changes
```

The second command is exactly what CI runs. It fails if the model has changes without a migration.

Rules:
- Descriptive migration names, for example `AddBookingTable`, not `Update1`.
- Never edit or delete a migration that is already on `main`. Add a new one instead.
- Commit the generated files in `Data/Migrations/` together with the entity change.

### 2.5 Tests (no CI check yet)

Applies once a test project exists. CI will get a `Test` check at that point.

```bash
dotnet test --configuration Release
```

### 2.6 All checks in one command

Stops at the first failure.

```bash
dotnet restore \
  && dotnet build --no-restore --configuration Release \
  && dotnet format --verify-no-changes \
  && ! dotnet list package --vulnerable --include-transitive | grep -q "has the following vulnerable packages" \
  && echo "All checks passed"
```

Once Entity Framework and tests are added, append before the `echo`:

```bash
  && dotnet ef migrations has-pending-model-changes \
  && dotnet test --configuration Release \
```

---

## 3. If a CI Check Fails

| Check | Fix |
|---|---|
| `Build` | Open the failed run in the **Actions** tab, fix the first error, re-run section 2.1. |
| `Format` | Run `dotnet format` and commit the changes. |
| `Vulnerable packages` | Find the listed package in the run log and update it (section 2.3). |
| `EF migrations` | Run `dotnet ef migrations add <Name>` and commit the migration files. |
| `Analyze (csharp)` (CodeQL) | Open **Security and quality → Code scanning**, read the alert (for example SQL injection or XSS), fix the code. Runs only while the repository is public; skipped when private. |

Pushing new commits to the same branch re-runs the checks.

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-18 | First version: pre-push checks, branch, commit, and pull request workflow. | Aleksandre Nadirashvili |
| 0.2 | 2026-10-18 | Removed branch, commit, and pull request sections. Kept only pre-push checks and CI fixes. | Aleksandre Nadirashvili |
| 0.3 | 2026-10-18 | Added CodeQL to the CI failure table. | Aleksandre Nadirashvili |
