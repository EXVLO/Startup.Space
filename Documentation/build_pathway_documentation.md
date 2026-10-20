# Build Pathway Documentation

> **Status:** Draft v0.1. The order to build the backend. Views are plain HTML only: no CSS, no JavaScript.
> Tables, pages, and folder names live in [`overview_documentation.md`](overview_documentation.md) and [`structure_documentation.md`](structure_documentation.md). Commands to run before a push live in [`development_workflow_documentation.md`](development_workflow_documentation.md).

Build one step, check that the project still builds, then go to the next step. Each step uses only the step before it.

```
1. Entity models
2. Database configuration
3. Migration
4. Repositories
5. Services
6. Wire the layers together
7. ViewModels
8. Controllers and plain HTML views
9. Check one page against the database
```

A request goes **Controller → Service → Repository → database**. The view only displays what the controller passes to it.

---

## 1. Entity models

Add one class per database table, under `Models/Entities/`.

Use the tables in Section 6 of the overview. Create a table’s class only after the tables it points to already exist: main tables first, then tables that belong to them, then join tables.

Each class is properties only: the id, the columns, and a link to the related entity where a foreign key exists. Leave out any table that Section 6 says does not exist yet.

---

## 2. Database configuration

Connect the entity classes to the database.

- One database context that lists every entity.
- One configuration per entity for keys, required fields, unique values, and relationships. These match Section 6 of the overview.
- A connection string for the local PostgreSQL database, kept out of source control.
- Register the context when the application starts, so the rest of the app can use it.

---

## 3. Migration

Generate the first migration from the entities and the configuration, then apply it so PostgreSQL has the tables.

After any later change to an entity or its configuration, add a new migration and apply that too. Commit the generated migration files with the change. Do not edit a migration that is already on `main`.

---

## 4. Repositories

Add repositories only after the migration has been applied.

Follow the repository list in the structure doc: one repository per entity group, each with an interface. A repository is the only layer that talks to the database context. Its methods read and write entities.

Start each repository with the methods the first pages need. Add more methods when a later page needs them.

---

## 5. Services

Add a service, with an interface, for each area in the structure doc. A service calls repositories. It does not talk to the database context.

The service is where a page’s rules live: what to load, what is allowed, and how entity data is turned into the shape a page needs.

---

## 6. Wire the layers together

Register every repository and service so a controller can receive the service it needs. Controllers depend on service interfaces. Services depend on repository interfaces.

---

## 7. ViewModels

Add a small class per page, under `Models/ViewModels/`. It contains only the fields that page shows. The service fills it from entities. A view never receives an entity.

---

## 8. Controllers and plain HTML views

Add one controller action per route in Section 5.2 of the overview.

The action calls the service and passes the ViewModel to the view. The view is a Razor page with ordinary HTML: text, links, lists, and forms. No stylesheet and no script. Shared layout is a simple header, navigation, and footer.

Build the public read pages first. Add forms (create, edit, login, register) after those pages show data from the database.

---

## 9. Check one page against the database

Put a few rows in the database, run the app, and open one list page. The page should show those rows.

When it does, repeat steps 4 to 8 for the next page. Steps 1 to 3 change only when a table or a column changes.

---

## Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-20 | First version: generalized backend build order, plain HTML views. | Aleksandre Nadirashvili |
