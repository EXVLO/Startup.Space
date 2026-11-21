# Local Testing Documentation

> **Status:** Draft v0.1.
> How to run the site on your machine and try the pages that exist. Pre-push checks stay in [`development_workflow_documentation.md`](development_workflow_documentation.md).
> Related: [`structure_documentation.md`](structure_documentation.md), [`overview_documentation.md`](overview_documentation.md).

---

Commands are run from the project root (the folder with `Startup.Lab.csproj`). You need Docker and the .NET SDK.

The app reads the database from `appsettings.Development.json` when you start it with `dotnet run`. That file points at PostgreSQL on `localhost`, port `5432`, database `startup_lab`, user `postgres`, password `postgres`.

## 1. Start the database

Create the container once:

```bash
docker run -d --name startup-lab-postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=startup_lab \
  -p 5432:5432 \
  postgres:16
```

After a reboot, or when the container is stopped, start the same one again:

```bash
docker start startup-lab-postgres
```

Check that it accepts connections:

```bash
docker exec startup-lab-postgres pg_isready -U postgres
```

The expected line is `/var/run/postgresql:5432 - accepting connections`.

If Register or Login fails with `Failed to connect to 127.0.0.1:5432` and `Connection refused`, the container is not running. Start it with the command above. The site process can stay running. Submit the form again.

## 2. Apply migrations

Run this the first time, and again after a new migration is added:

```bash
dotnet ef database update
```

Install the tool first if the command is missing: `dotnet tool install --global dotnet-ef`.

## 3. Run the site

```bash
dotnet run
```

Open `http://localhost:5226`. `dotnet run` uses the Development settings, including the connection string. Starting the app without that environment leaves the connection string empty and registration fails before it can reach PostgreSQL.

## 4. Try the pages

| Page | Address | What to check |
|---|---|---|
| Home | `/` | Login, Register, Profile, and Settings buttons |
| Register | `/Register` | Name, surname, email, password, and repeat password. A new account is signed in and sent to Profile |
| Login | `/Login` | Email and password. A match is sent to Profile |
| Profile | `/Profile` | Name, photo address key, contacts, and experience. Signed-out visitors go to Login |
| Settings | `/Settings` | Deactivate or delete the signed-in account. Delete signs you out and returns to Home. A leftover sign-in cookie for a deleted account is cleared and sent to Login |
| Logout | button in the top bar | Shown only while signed in. Returns to Home |

The password must be at least 6 characters and include an uppercase letter, a lowercase letter, a number, and a symbol. Example: `Test123!`.

A repeated password that does not match stays on the Register page with the message "The passwords do not match." The name, surname, and email stay filled in.

An inactive account cannot sign in. Deactivate on Settings, log out, and try Login with the same email.

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-11-21 | How to start the local PostgreSQL container, apply migrations, run the site, and try register, login, profile, and settings. | Aleksandre Nadirashvili |
