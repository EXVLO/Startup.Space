# Project Overview Documentation

> **Status:** Draft v0.7, written from the updated `HSS PRoject 1.xlsx` (2026-10-24) plus team decisions made after the first version.
> Where a team decision changes the Excel, the text says so and links to [Section 7.1, Role model](#71-role-model-team-decision-v05). Everything else comes from the Excel. Anything unclear, missing, or contradictory is listed in [Section 11, Open Questions](#11-open-questions) and will be answered later, when each feature is built.
> **Current goal:** set up the layers and folder/file structure only (see `structure_documentation.md`). No business logic yet, and no code has been written from this document.

---

## 1. Source

| Item | Value |
|---|---|
| Source file | `Documentation/HSS PRoject 1.xlsx` |
| Sheets in file | 17 |
| Sheet names | Pages, Sheet23, Tables, Routes, Register - Login Page, Founders Toolkit Page, Landing Page, Startups List Page, My Startups Page, Founders List Page, Startup Detail Page, Founders Details Page, News Page, Booking Page, Archive -- inactive, About Us Page, Privacy and Policy Page |
| Removed in the 2026-10-24 version | **Extra Info - Devs** (deleted) and **Black Sheet** (now an empty sheet named "Sheet23"). Sections 3 and 4 keep their content from the earlier version (Q-GEN-10). |
| Added in the 2026-10-24 version | **My Startups Page** |

---

## 2. Project Summary (as described so far)

A website where **students who have their own startups (founders)** can upload their startups so other people can see them. The platform also has extra services, including **booking sessions with crew members / mentors** for advice, a **Founders Toolkit** (pathways, video tutorials, documents, glossary, checklist), and a **News feed**.

There is one kind of account: **User**. Every user is treated as a founder. An admin can give any user the **mentor** role (see Section 7.1).

- Project name: **Startup.Lab** (also used in the Landing Page sheet, "About Startup.Lab"). It was called MentorBooking until 2026-10-24.
- Domain in the Routes sheet: **`incubation.harbour.space`**
- Project folder and file: `Startup.Lab/`, `Startup.Lab.csproj`. The C# root namespace is `StartupLab`, because a namespace named `Startup` would hide the `Startup` entity class.
- GitHub repository: `EXVLO/Startup.Space`

---

## 3. Technical Stack and Architecture (sheet "Extra Info - Devs")

> This sheet is not in the 2026-10-24 Excel. The content below is from the earlier version and still applies until Q-GEN-10 says otherwise.

| Area | Specified |
|---|---|
| Language | C# |
| Framework | ASP.NET, **.NET 9.0** |
| Project structure | Back end and front end in **one project** |
| UI pattern | **MVC structure / Razor views** |
| Front-end | JS, CSS, HTML |
| Database | **PostgreSQL** |
| ORM | Entity Framework (EF) |
| Architecture | **Layered architecture** |

### 3.1 Layer flow (left to right, exactly as listed)

```
Routing -> Controller -> Services -> Repositories -> EF -> DB (PostgreSQL)
                                                          -> Razor views
```

> Note: the existing `Startup.Lab.csproj` targets **`net10.0`**, but the Excel says **.NET 9.0**. See question Q-TECH-1.

### 3.2 Explicit technical requirements

- No N+1 queries
- Secure authentication and authorization
- API rate limiting

### 3.3 "Requirements to consider" (copied from the sheet)

**Security and access**
- **Authentication:** How users sign in, sign out, and recover accounts. Prefer ASP.NET Core Identity or a trusted identity provider over custom password handling.
- **Authorization:** Check permissions on the server for every protected action, not only by hiding buttons in the UI. Use roles or policies where appropriate.
- **Input validation:** Validate on the server, even if you also validate in the browser.
- **CSRF protection:** Use antiforgery tokens for cookie-authenticated form submissions.
- **Output encoding:** Do not render user-provided content as trusted HTML (XSS risk).
- **Secrets management:** Keep passwords, API keys, and connection strings out of source control.
- **Dependency updates:** Keep .NET and third-party packages patched.
- **Transport security:** HTTPS, secure cookie settings, sensible security headers.

**Data and performance**
- **Avoid N+1 queries:** Use proper EF Core loading/projection patterns. Check the generated SQL.
- **Database constraints:** Enforce required fields, unique values, and relationships in the DB too.
- **Pagination and limits:** Do not return unbounded lists.
- **Indexes:** Add them based on real query patterns.
- **Caching:** Define what can be cached and for how long. Never cache user-specific or sensitive responses by accident.
- **Rate limiting:** Especially on sign-in, account recovery, and expensive or abuse-prone operations.

**Reliability and user experience**
- **Error handling:** Show understandable messages. Keep exception details out of public responses.
- **Logging and monitoring:** Never log passwords, tokens, or unnecessary personal data. Track health and latency.
- **Backups and recovery:** Define how often the DB is backed up and how restores are done and tested.
- **Accessibility:** Keyboard navigation, visible focus, labels, contrast, semantic HTML. Target: WCAG 2.2 AA.
- **Responsive design:** Specify the supported screen sizes, browsers, and devices.
- **Localization and time zones:** Decide on languages, currencies, and date/time formats.

**Delivery and maintenance**
- **Environments:** Separate dev, test, and prod settings.
- **Testing:** Unit tests (business rules), integration tests (DB/API), end-to-end tests (critical journeys).
- **Deployment and migrations:** A repeatable deployment process and safe schema changes.
- **Retention and privacy:** Decide what personal data is collected, why, who can access it, and when it is deleted.
- **Documentation:** Setup instructions, design decisions, operational procedures.

**Questions still open in the sheet itself**
- What are the expected traffic and response-time targets?
- Which pages or endpoints need rate limits?

---

## 4. Feature Map (sheet "Black Sheet")

> In the 2026-10-24 Excel this sheet is empty and renamed "Sheet23". The map below is from the earlier version (Q-GEN-10).

| Main page | Detail / sub pages |
|---|---|
| Founders List | |
| Startups List | |
| Founder Profile | |
| Startup Details | |
| Archive | Archive - Startups, Archive - Founders |
| Website Introduction / Landing Page | |
| Founders Toolkit | Checklist / Documents / requirements, Video Tutorials, Glossary |
| Update feed | |
| Session Booking | Booking with Investor, Booking with Crew team (Tutorials) |
| Login / Sign in page | |
| Privacy and Policy Page | |
| Crew | |
| Investors **(Optional)** | |

---

## 5. Pages

### 5.1 Page list and status (sheet "Pages")

| # | Page | Status in sheet | Sub-items listed next to it |
|---|---|---|---|
| 1 | Login/Register Page | Done | |
| 2 | Landing Page | Done | |
| 3 | Startups List Page | Done | |
| 4 | Founders List Page | Done | |
| 5 | Startup Details Page | Done | |
| 6 | Founders Details Page | Done | |
| 7 | Booking Page / With Crew Members | Done | |
| 8 | News Feed Page | Done | |
| 9 | Founders Toolkit Page | Done | Video Tutorials Page (Udemy), Founder Pathways Page |
| 10 | About Us Page | Done | Team & Mentors |
| 11 | Privacy and Policy Page | Done | |
| 12 | Profile | *(no status)* | |
| 13 | Settings | *(no status)* | |
| 14 | My Startups | *(no status)* | Listed as "my startups" under Profile/Settings. Has its own wireframe sheet and route since 2026-10-24 |

> "Done" is copied from the sheet. What it means is question Q-GEN-3.

### 5.2 Routes (sheet "Routes")

Base domain: `incubation.harbour.space`

| # | Route | Page |
|---|---|---|
| 1 | `/` | Landing Page |
| 2 | `/Login` | Login |
| 3 | `/Register` | Register |
| 4 | `/Startups` | Startup List |
| 5 | `/Startups/{id}` | Startup Details |
| 6 | `/Founders` | Founder List |
| 7 | `/Founders/{id}` | Founder Details |
| 8 | `/News` | News |
| 9 | `/AboutUs` | About Us |
| 10 | `/AboutUs/Team` | About Us / Team & Mentors |
| 11 | `/Profile` | Profile |
| 12 | `/Settings` | Settings |
| 13 | `/Booking` | Booking |
| 14 | `/Archive` | Archive |
| 15 | `/PP` | Privacy and Policy |
| 16 | `/Toolkit` | Founders Toolkit |
| 17 | `/Toolkit/Videos` | Toolkit / Video Tutorials |
| 18 | `/Toolkit/Pathway` | Toolkit / Pathway |
| 19 | `/MyStartups` | My Startups |

### 5.3 Common layout

Every wireframe sheet except Register/Login and Privacy and Policy shows the same frame: **Header** (4 items), **Nav Bar**, page content, **Footer** (4 items). The header, nav bar, and footer items are only drawn as "…" placeholders.

### 5.4 Page-by-page details (from wireframe sheets)

Access labels are copied exactly as written in cell A1/A2 of each sheet.

#### Register / Login Page (Access: Public)
- **Login form:** Email, Password (with a note "Tvali", Georgian for "eye", which may mean a show/hide password toggle; see Q-AUTH-2), Login button
- Links: **Forgot Password**, **Create Account**
- **Register form:** Name, Surname, Email, Password (note "tvali"), Repeat password
- ~~Questions on the register form: "Are you a founder" (note: "found") and "Are you a mentor"~~. **Removed (v0.5):** registration no longer asks about roles. Every new account is a normal user, and only an admin can give the mentor role (Section 7.1). The 2026-10-24 sheet still shows both questions, with a new note under them: **"normal user no founder no mentor"**.

#### Landing Page (Access: Public)
Sections in order:
1. **About Startup.Lab**
2. **Residency & Journey**
3. **Highlighted startups**, with a link "More about Startups" to the Startups List Page
4. **Meet the Crew**, with a link "More about Crew" to the About Us Page

#### Startups List Page (Access: Public)
- Grid of startup cards (wireframe shows 7: Startup 1 to 7)
- **Search bar**
- **Filter by founders, filter by tags**

#### Founders List Page (Access: Public)
- Grid of founder cards (wireframe shows 7: Founder 1 to 7)
- **Search bar**
- Since v0.5, "founder" means any user, because there is no separate founder flag any more (Section 7.1). Whether mentors also appear here is Q-FD-7.

#### Startup Details Page (Access: Public)
Sections in order:
1. **"Who did it" (User)**: the founder(s) of the startup
2. **Info**
3. **Documents**
4. **Contact Info**
5. **Website Link** ("whatever it is")
6. **Photos/Videos**

#### Founders Details Page (Access: Public)
- **Founder photo** and **Info about founder**
- **Founder's startups** (wireframe shows 3 cards) plus **"More startups by this founder"**. Clicking it opens the Startups List already filtered by this founder's name.
- **Contact Info**

#### News Page (Access: Public)
- A **thumbnail** / featured area at the top. The earlier Tables sheet had a note "main news". The 2026-10-24 version replaced it with a `News.is_primary` column, which probably marks this featured item (Q-NEWS-2).
- A grid of News items
- **Paginated**
- **Filter by tags:** since 2026-10-24, tags are stored in the `Tag` table and linked through `News_Tag` (Sections 6.6 and 6.7)

#### Booking Page (Access: user only)
- Title: "Booking Crew Page"
- **Book with crew member / mentors**
- **Calendar**
- **Session Guidelines**
- Note in cell C1: only **founders** may use booking (cell C1 is a crude note, roughly "if not a founder, no access"). Since v0.5 every user is a founder, so **any logged-in user** can use this page (Section 7.1).
- The people being booked are **mentors** (users with `is_mentor = true`). The Excel calls them crew members.
- **Mentor booking page (WIP, v0.5):** mentors get access to a separate booking page. Its content and route are not decided yet (Q-BOOK-14).
- Crew member (mentor) availability states shown in the wireframe:

| State | Label | Extra text |
|---|---|---|
| 1 | inactive / active | |
| 2 | busy / available | "busy until …" / "available until …" |
| 3 | contact crew | |

#### Founders Toolkit Page (Access: "public/user required")
- Sub-navigation: **Founder Pathways**, **Video Tutorials**, **View Your Checklist Page**
- Note: **"mentor cannot checklist"** (mentors cannot use the checklist). Since a mentor is now also a normal user, this rule needs re-confirming (Q-TK-6).
- Content sections: **Founder Pathways**, **Requirements**, **Documents**, **Glossary**
- Pages sheet: Video Tutorials are labelled **"(Udemy)"**

#### About Us Page (Access: Public)
- **Info about this website and crews**
- Crew Member cards (wireframe shows 3)
- Route `/AboutUs/Team` shows "Team & Mentors"
- Since v0.5 the crew/mentor cards come from users with `is_mentor = true`, because the `Crew Member` table has been removed (Section 6.5).

#### Archive (Access: Public)
- Sheet name: "Archive -- inactive". The sheet is otherwise empty.
- The Black Sheet splits it into **Archive - Startups** and **Archive - Founders**

#### Privacy and Policy Page (Access: Public)
- Title only. No content specified.

#### My Startups Page (Access: "private/user"; added 2026-10-24)
- Route: `/MyStartups`
- Title: "My Startups Page"
- Grid of the logged-in user's startup cards (wireframe shows 7: Startup 1 to 7)
- **Search bar**
- **Filter by tags.** Startups have `Category`, not `Tag`; see Q-SU-14.
- Creating and editing startups is not shown on the wireframe (Q-PR-2).

#### Profile and Settings
- Listed in the Pages sheet and in Routes (`/Profile`, `/Settings`).
- The "profile" sub-item was removed from the Pages sheet on 2026-10-24. "my startups" is now its own page (above).
- No wireframe sheet exists for either page.

---

## 6. Data Model (sheet "Tables")

Columns are copied exactly from the 2026-10-24 Excel. That version already includes the role model changes (Section 7.1). **PK** = primary key, **FK** = foreign key, **nullable** = optional. Unmarked columns are assumed **not nullable** because the sheet only marks nullable ones; confirm in Q-DATA-1.

### 6.1 User group

**User**
| # | Column | Notes |
|---|---|---|
| 1 | user_id | PK |
| 2 | name | |
| 3 | surname | |
| 4 | email | |
| 5 | date_created | |
| 6 | date_modified | nullable |
| 7 | description | nullable |
| 8 | is_mentor | default `false`. **Added v0.5.** Only an admin can change it. |
| 9 | is_admin | |
| 10 | is_active | |

> **Removed v0.5:** `is_founder`. Every user is a founder, so the flag is not needed.
> **Removed 2026-10-24:** `startup_count`. The count comes from `User_Startup`. In the sheet the row numbers skip from 7 to 9 where it used to be.

**User Photo**
| # | Column | Notes |
|---|---|---|
| 1 | user_photo_id | PK |
| 2 | user_id | FK to User |
| 3 | address_key | |
| 4 | date_created | |

**User Contact**
| # | Column | Notes |
|---|---|---|
| 1 | user_contact_id | PK |
| 2 | user_id | FK to User |
| 3 | contact_info | |
| 4 | date_created | |
| 5 | date_modified | nullable |

**User Experience** (**added v0.5**; replaces the "experience" note above the removed `Crew Member` table)
| # | Column | Notes |
|---|---|---|
| 1 | user_exp_id | PK |
| 2 | user_id | FK to User |
| 3 | title | nullable |
| 4 | description | nullable |
| 5 | date_created | |
| 6 | date_modified | |

One user can have many experience entries. Any user can add them, not only mentors. `date_modified` is not marked nullable here, unlike on most other tables (Q-DATA-6).

### 6.2 Startup group

**Startup**
| # | Column | Notes |
|---|---|---|
| 1 | startup_id | PK |
| 2 | name | |
| 3 | description | nullable |
| 4 | date_created | |
| 5 | date_modified | nullable |
| 6 | is_active | |

**Startup Photo**
| # | Column | Notes |
|---|---|---|
| 1 | startup_photo_id | PK |
| 2 | startup_id | FK to Startup |
| 3 | address_key | |
| 4 | title | nullable |
| 5 | description | nullable |
| 6 | display_order | |
| 7 | is_primary | |
| 8 | date_created | |

**Startup Video**
| # | Column | Notes |
|---|---|---|
| 1 | startup_video_id | PK |
| 2 | startup_id | FK to Startup |
| 3 | address_key | |
| 4 | title | nullable |
| 5 | description | nullable |
| 6 | display_order | |
| 7 | date_created | |

**Startup Document**
| # | Column | Notes |
|---|---|---|
| 1 | startup_document_id | PK |
| 2 | startup_id | FK to Startup |
| 3 | address_key | |
| 4 | title | nullable |
| 5 | description | nullable |
| 6 | display_order | |
| 7 | date_created | |

**Startup Link**
| # | Column | Notes |
|---|---|---|
| 1 | startup_link_id | PK |
| 2 | startup_id | FK to Startup |
| 3 | link | |
| 4 | title | nullable |
| 5 | description | nullable |
| 6 | display_order | |
| 7 | date_created | |

### 6.3 Toolkit group

**Toolkit Video**
| # | Column | Notes |
|---|---|---|
| 1 | toolkit_video_id | PK |
| 2 | address_key | |
| 3 | title | nullable |
| 4 | description | nullable |
| 5 | display_order | |
| 6 | date_created | |

**Toolkit Document**
| # | Column | Notes |
|---|---|---|
| 1 | toolkit_document_id | PK |
| 2 | address_key | |
| 3 | title | nullable |
| 4 | description | nullable |
| 5 | display_order | |
| 6 | date_created | |

**Toolkit Glossary**
| # | Column | Notes |
|---|---|---|
| 1 | toolkit_glossary_id | PK |
| 2 | address_key | |
| 3 | term | |
| 4 | definition | |
| 5 | display_order | |
| 6 | date_created | |
| 7 | date_modified | nullable (marked on 2026-10-24) |

**Toolkit Checklist**
| # | Column | Notes |
|---|---|---|
| 1 | toolkit_checklist_id | PK |
| 2 | label | |
| 3 | display_order | |
| 4 | is_active | |

### 6.4 News group

**News**
| # | Column | Notes |
|---|---|---|
| 1 | news_id | PK |
| 2 | title | |
| 3 | description | nullable |
| 4 | date_created | |
| 5 | date_modified | nullable |
| 6 | is_primary | Was `is_active` before 2026-10-24. Probably marks the featured "main news" item (Q-NEWS-2, Q-NEWS-7). |

**News Photo**
| # | Column | Notes |
|---|---|---|
| 1 | news_photo_id | PK |
| 2 | news_id | FK to News |
| 3 | title | nullable |
| 4 | description | nullable |
| 5 | address_key | |
| 6 | display_order | |
| 7 | is_primary | |
| 8 | date_created | |

**News Video**
| # | Column | Notes |
|---|---|---|
| 1 | news_video_id | PK |
| 2 | news_id | FK to News |
| 3 | title | nullable |
| 4 | description | nullable |
| 5 | address_key | |
| 6 | display_order | |
| 7 | is_primary | |
| 8 | date_created | |

### 6.5 Crew group (removed in v0.5)

The Excel had a **Crew Member** table (`member_id`, name, surname, email, dates, description, is_active) and a **Crew Member Photo** table, with the notes **"mentor"**, **"links"**, **"experience"** written above them. Both tables are **removed**. Mentors are now normal users with `User.is_mentor = true` (Section 7.1):

| Old Crew Member data | Where it lives now |
|---|---|
| name, surname, email, description, dates, is_active | the same columns on `User` |
| "mentor" note | `User.is_mentor` |
| "experience" note | `User Experience` table (many per user) |
| photos (Crew Member Photo) | `User Photo` |
| "links" note | not decided; `User Contact` may cover it (Q-CREW-3) |

### 6.6 Category and Tag group

**Category** (used by Startups)
| # | Column | Notes |
|---|---|---|
| 1 | category_id | PK |
| 2 | title | |
| 3 | description | nullable |
| 4 | date_created | |
| 5 | date_modified | nullable (marked on 2026-10-24) |
| 6 | is_active | |

**Tag** (added 2026-10-24; used by News through `News_Tag`)
| # | Column | Notes |
|---|---|---|
| 1 | tag_id | PK |
| 2 | title | |
| 3 | description | nullable |
| 4 | date_created | |
| 5 | date_modified | nullable |
| 6 | is_active | |

### 6.7 Pivot (join) tables

**Startup_Category** (many-to-many: Startup and Category)
| # | Column | Notes |
|---|---|---|
| 1 | startup_id | FK to Startup |
| 2 | category_id | FK to Category |

**News_Tag** (many-to-many: News and Tag; added 2026-10-24)
| # | Column | Notes |
|---|---|---|
| 1 | news_id | FK to News |
| 2 | tag_id | FK to Tag |

**User_Checklist** (many-to-many: User and Toolkit Checklist, with progress)
| # | Column | Notes |
|---|---|---|
| 1 | user_id | FK to User |
| 2 | toolkit_checklist_id | FK to Toolkit Checklist |
| 3 | is_completed | |
| 4 | date_completed | |

**User_Startup** (many-to-many: User and Startup)
| # | Column | Notes |
|---|---|---|
| 1 | user_id | FK to User |
| 2 | startup_id | FK to Startup |

### 6.8 Relationships (directly from the FKs)

```
User            1 ── * User Photo
User            1 ── * User Contact
User            1 ── * User Experience
User            * ── * Startup            (via User_Startup)
User            * ── * Toolkit Checklist  (via User_Checklist)
Startup         1 ── * Startup Photo / Startup Video / Startup Document / Startup Link
Startup         * ── * Category           (via Startup_Category)
News            1 ── * News Photo / News Video
News            * ── * Tag                (via News_Tag)
```

Toolkit Video, Toolkit Document, and Toolkit Glossary have no foreign keys.

### 6.9 Things the pages need that have no table yet

These are listed as facts. They are **not** design decisions. Each one is linked to a question.

- **Booking / appointments / mentor availability.** The Booking page has a calendar and busy/available states, but no table exists. A booking would link a user to a mentor (both rows in `User`) (Q-BOOK-3).
- **Investors.** In the Black Sheet as "(Optional)" with "Booking with Investor", but no table (Q-INV-1).
- **Tags for Startups.** News now has `Tag` / `News_Tag` (added 2026-10-24). The Startups List and My Startups pages also say "filter by tags", but startups only link to `Category` (Q-DATA-4, Q-SU-14).
- **Founder Pathways / Requirements.** Toolkit page sections without tables (Q-TK-2).
- **Password / authentication data.** The User table has no password or auth fields (Q-AUTH-1).
- ~~**Mentor flag.**~~ **Resolved v0.5:** `User.is_mentor` added.

---

## 7. Roles and Access

### 7.1 Role model (team decision, v0.5)

This replaces the founder/mentor/crew split from the Excel.

| Role | How it is stored | What it can do |
|---|---|---|
| Visitor | not logged in | Public pages only |
| User (= founder) | a row in `User` | Everything a logged-in user can do: profile, own startups, toolkit, booking a mentor, etc. There is no separate founder flag. |
| Mentor | `User.is_mentor = true` | Everything a user can do, plus access to the separate **mentor booking page** (WIP, Q-BOOK-14). Shown as crew/mentors on About Us and Landing. |
| Admin | `User.is_admin = true` | Can give the mentor role to **any** account. Other admin abilities: Q-ROLE-4. |

Flow:

1. Someone registers. Registration does not ask about roles, so they become a normal user (founder).
2. The user uses the site normally.
3. Later, an admin can set `is_mentor = true` on that account. Users can't do this themselves.
4. As a mentor, the account can also open the mentor booking page.

Consequences:

- `User.is_founder` is removed, `User.is_mentor` is added, and a new `User Experience` table is added (Section 6.1).
- The `Crew Member` and `Crew Member Photo` tables are removed (Section 6.5).
- The register form no longer asks "Are you a founder" / "Are you a mentor" (Section 5.4).

The 2026-10-24 Excel matches this for the data model: `is_mentor` replaces `is_founder`, Crew Member is marked "Deleted", and `User Experience` is added.

### 7.2 Roles as written in the Excel (historical)

Kept for reference. Section 7.1 replaces the founder, mentor, and crew rows.

| Role (as named in the Excel) | Where it appears |
|---|---|
| Public / anonymous visitor | Most pages are labelled "Public" |
| User (logged in) | Booking "user only", Toolkit "public/user required" |
| Founder | `User.is_founder`, register "Are you a founder", Booking note |
| Mentor | Register "Are you a mentor", "mentor cannot checklist", "Team & Mentors", "Book with crew member / mentors" |
| Crew member | `Crew Member` table, "Meet the Crew", "Booking with Crew team" |
| Admin | `User.is_admin` |
| Investor (optional) | Black Sheet only |

### 7.3 Page access

| Page | Access label in sheet |
|---|---|
| Register / Login | Public |
| Landing | Public |
| Startups List | Public |
| Founders List | Public |
| Startup Details | Public |
| Founders Details | Public |
| News | Public |
| About Us | Public |
| Archive | Public |
| Privacy and Policy | Public |
| Founders Toolkit | public/user required |
| Booking | user only (founders only, per note). Since v0.5: any logged-in user |
| Mentor booking page | *(not in the sheet; v0.5)* Mentors only. WIP |
| My Startups | private/user (added 2026-10-24) |
| Profile | *(not specified)* |
| Settings | *(not specified)* |

---

## 8. Inconsistencies Found Between Sheets

1. **.NET version:** the Excel says .NET 9.0, but the project targets `net10.0`.
2. **Archive:** in the Black Sheet and Routes, but **not** in the Pages sheet.
3. **Investors / Booking with Investor:** in the Black Sheet only. Not in Pages, Routes, or Tables.
4. **Toolkit sub-pages:** the Black Sheet has *Checklist / Documents / requirements, Video Tutorials, Glossary*. The Pages sheet has *Video Tutorials, Founder Pathways*. Routes only has `/Toolkit/Videos` and `/Toolkit/Pathway`. The Toolkit wireframe has *Founder Pathways, Video Tutorials, View Your Checklist Page* plus sections *Requirements, Documents, Glossary*.
5. **"Update feed" vs "News Feed" vs "News":** possibly the same feature under three names.
6. **"Founder Profile" vs "Founders Details Page":** possibly the same page.
7. ~~**"Crew" vs "Mentors" vs "Team & Mentors"**~~ **Resolved v0.5:** all are users with `is_mentor = true`.
8. ~~**Mentor role**~~ **Resolved v0.5:** stored as `User.is_mentor` and set by an admin. The Crew Member table is removed.
9. **Booking page:** it is listed as "Booking Page / With Crew Members" in Pages, but the Black Sheet also has "Booking with Crew team (Tutorials)" and "Booking with Investor".
10. ~~**`startup_count` on User**~~ **Resolved 2026-10-24:** the column was removed.
11. **Startup "Contact Info" section:** there is no startup contact table, only `User Contact`.
12. **`Toolkit Glossary.address_key`:** the glossary has term/definition, and it's unclear what file it would point to.
13. ~~**Toolkit Glossary and Category `date_modified`**~~ **Resolved 2026-10-24:** both are now marked nullable. `User Experience.date_modified` is the only `date_modified` still not marked nullable.
14. **News `is_active` removed (2026-10-24):** News now has `is_primary` in that place. Every other main table (User, Startup, Category, Tag) has `is_active`, so news can no longer be hidden or soft-deleted (Q-NEWS-7).
15. **Register sheet (2026-10-24):** it still shows "Are you a founder" / "Are you a mentor", but the new note "normal user no founder no mentor" and the Tables sheet say there are no role questions.
16. **"Filter by tags" for startups:** shown on Startups List and My Startups, but `Tag` is only linked to News. Startups use `Category`.
17. **Missing sheets (2026-10-24):** "Extra Info - Devs" was deleted and "Black Sheet" is empty ("Sheet23"). Sections 3 and 4 still rely on their earlier content.

---

## 9. Project Structure

Moved to [`structure_documentation.md`](structure_documentation.md).

---

## 10. Change Log

| Version | Date | Change | Author |
|---|---|---|---|
| 0.1 | 2026-10-17 | First draft from `HSS PRoject 1.xlsx`. Open questions listed. | Aleksandre Nadirashvili |
| 0.2 | 2026-10-17 | Added Section 9, Project Structure (layers, folders, files). Open questions moved to the end, to be answered per feature. | Aleksandre Nadirashvili |
| 0.3 | 2026-10-17 | Project structure moved to `structure_documentation.md`. | Aleksandre Nadirashvili |
| 0.4 | 2026-10-17 | Added Author column to the change log. | Aleksandre Nadirashvili |
| 0.5 | 2026-10-23 | New role model (Section 7.1): one kind of account (User = founder), admin-assigned mentor role. Removed `User.is_founder` and the Crew Member / Crew Member Photo tables. Added `User.is_mentor` and the `User Experience` table. Removed role questions from registration. Added the mentor booking page (WIP). Updated the affected pages, inconsistencies, and open questions. | Aleksandre Nadirashvili |
| 0.6 | 2026-10-24 | Synced with the updated Excel (2026-10-24). Removed `User.startup_count`. Changed `News.is_active` to `is_primary`. Added the `Tag` and `News_Tag` tables. Marked as nullable: Toolkit Glossary and Category `date_modified`, and User Experience `title`/`description`. Added the My Startups page and the `/MyStartups` route. Noted the deleted "Extra Info - Devs" sheet and the emptied "Black Sheet". Updated inconsistencies and open questions. | Aleksandre Nadirashvili |
| 0.7 | 2026-10-24 | Project renamed from MentorBooking to Startup.Lab (Q-GEN-1). | Aleksandre Nadirashvili |

---

## 11. Open Questions

These don't block the folder structure. They will be answered when each feature is built. Answer format: "Q-GEN-1: …".

### 11.1 General / product
- ~~**Q-GEN-1:** What is the official product name?~~ **Answered (2026-10-24):** Startup.Lab.
- **Q-GEN-2:** Is this for Harbour.Space (domain `incubation.harbour.space`)? Who is the organization behind it?
- **Q-GEN-3:** What does "Done" in the Pages sheet mean: wireframe designed, requirements final, or something else?
- **Q-GEN-4:** Are Profile, Settings, and My Startups in scope for the first version? They have no status.
- **Q-GEN-5:** Is there an MVP / first-release scope, and what's in it? Is anything "optional" besides Investors?
- **Q-GEN-6:** Are there designs (Figma etc.) beyond the Excel wireframes, or does the Excel define the layout?
- **Q-GEN-7:** What goes in the Header (4 items), Nav Bar, and Footer (4 items)?
- **Q-GEN-8:** What text/content goes in "About Startup.Lab", "Residency & Journey", About Us, and Privacy Policy? Will you provide it, or should it be placeholder text?
- **Q-GEN-9:** What does "Residency & Journey" mean for this program?
- **Q-GEN-10:** In the 2026-10-24 Excel, the "Extra Info - Devs" sheet is deleted and the "Black Sheet" is empty ("Sheet23"). Was that intended? Does their earlier content (tech stack, layers, requirements, feature map) still apply?

### 11.2 Tech
- **Q-TECH-1:** .NET 9.0 (Excel) or .NET 10 (current project)?
- **Q-TECH-2:** Where will it be hosted, and where will the PostgreSQL database run?
- **Q-TECH-3:** Should ASP.NET Core Identity be used for authentication (the Excel recommends "Identity or a trusted provider")? Is any external login wanted (Google, university SSO)?
- **Q-TECH-4:** Is there a front-end CSS framework preference (Bootstrap is in the template, Tailwind, none)?
- **Q-TECH-5:** Are expected traffic and response-time targets known? (This question is open in the sheet.)
- **Q-TECH-6:** Which endpoints need rate limits, and with what limits? (Also open in the sheet.)
- **Q-TECH-7:** Which testing level is required now: unit, integration, end-to-end?
- **Q-TECH-8:** Supported browsers/devices? Must it be mobile-responsive?
- **Q-TECH-9:** Languages: English only? Any time zone requirement for bookings?

### 11.3 Authentication and registration
- **Q-AUTH-1:** The User table has no password/security fields. Should Identity's own tables hold them, with `User` as a profile table linked to them?
- **Q-AUTH-2:** Does "Tvali" next to the password fields mean a show/hide-password ("eye") toggle?
- ~~**Q-AUTH-3:** What does "found" next to "Are you a founder" mean?~~ **Obsolete (v0.5):** the question was removed from registration.
- ~~**Q-AUTH-4:** Can a user be both a founder and a mentor?~~ **Answered (v0.5):** yes. Every user is a founder, and a mentor is a user with `is_mentor = true`.
- ~~**Q-AUTH-5:** If someone ticks "Are you a mentor", are they a mentor immediately, or does an admin have to approve them?~~ **Answered (v0.5):** there is no checkbox; only an admin can give the mentor role.
- **Q-AUTH-6:** Is email confirmation required at registration?
- **Q-AUTH-7:** How should Forgot Password work (email reset link)? Which email service should send it?
- **Q-AUTH-8:** Can anyone register, or only students/people with a specific email domain?

### 11.4 Roles and permissions
- ~~**Q-ROLE-1:** Please confirm the full list of roles.~~ **Answered (v0.5):** visitor, user (= founder), mentor, admin. Investor is still Q-INV-1.
- ~~**Q-ROLE-2:** How is "mentor" stored?~~ **Answered (v0.5):** `User.is_mentor`.
- ~~**Q-ROLE-3:** Are crew members/mentors users who log in, or only admin-managed profiles?~~ **Answered (v0.5):** they are users who log in. The `Crew Member` table is removed.
- **Q-ROLE-4:** What can an admin do besides giving the mentor role (decided in v0.5)? Is an admin panel needed (manage users, startups, news, toolkit, categories), and what is its route? It isn't in the pages list.
- ~~**Q-ROLE-5:** What can a logged-in non-founder, non-mentor user do?~~ **Obsolete (v0.5):** every user is a founder.
- **Q-ROLE-6:** "public/user required" on the Toolkit: which parts are public and which need login?
- **Q-ROLE-7:** Can an admin remove the mentor role again? If so, what happens to that mentor's existing bookings?
- **Q-ROLE-8:** Can an admin give or remove the admin role, or is `is_admin` set only directly in the database?

### 11.5 Startups
- ~~**Q-SU-1:** Who can create a startup? Only users with `is_founder`?~~ **Answered (v0.5):** any user (there is no `is_founder` any more).
- **Q-SU-2:** Can a startup have multiple founders (`User_Startup` suggests yes)? How are co-founders added: invitation, search, approval?
- **Q-SU-3:** Does an admin need to approve a startup before it is public?
- **Q-SU-4:** Who can edit or delete a startup: any co-founder, only the creator, an admin?
- **Q-SU-5:** What fields go in the "Info" section beyond name and description (stage, year, industry, team size, etc.)?
- **Q-SU-6:** Where does the startup's "Contact Info" come from: the founders' User Contact, or a separate startup contact?
- **Q-SU-7:** "Website Link whatever it is": is that a single main link or multiple links (the `Startup Link` table allows many)?
- **Q-SU-8:** What are the allowed file types and size limits for photos, videos, and documents? Are videos uploaded files or links (YouTube, etc.)?
- **Q-SU-9:** What makes a startup "Highlighted" on the Landing page? There is no field for it.
- **Q-SU-10:** What does "Filter by founders" look like: a dropdown, multi-select, or search?
- **Q-SU-11:** What does the search bar search: name, description, founder, categories?
- **Q-SU-12:** How many startups per page? Should the list be paginated?
- ~~**Q-SU-13:** What should happen to `startup_count`?~~ **Answered (2026-10-24):** removed from User. Calculate it from `User_Startup`.
- **Q-SU-14:** "Filter by tags" on Startups List and My Startups: should it use `Category`, or should startups also link to `Tag` (e.g. a `Startup_Tag` table)?
- **Q-SU-15:** My Startups page: does the search bar search only the user's own startups? Are inactive (`is_active = false`) startups shown there?

### 11.6 Founders
- ~~**Q-FD-1:** Is the Founders List only users with `is_founder = true`?~~ **Answered (v0.5):** `is_founder` is removed; the list shows users (see Q-FD-7 for mentors).
- **Q-FD-2:** What "Info about founder" is shown beyond name, surname, and description (university, program, year, skills)?
- **Q-FD-3:** What is `contact_info` in User Contact: free text, or typed (email, phone, LinkedIn, etc.)? Can there be many per user?
- **Q-FD-4:** Can a user have multiple photos (the table allows it), or only one profile photo?
- **Q-FD-5:** What does the founder search bar search?
- **Q-FD-6:** Should founders' email/contact be visible to the public or only to logged-in users?
- **Q-FD-7:** Do mentors also appear in the Founders List, or only in the crew/mentor sections? Should users with no startups appear?
- **Q-FD-8:** Are `User Experience` entries shown on the Founder Details page for every user, or only for mentors? In what order (there is no `display_order`)?

### 11.7 Archive
- **Q-AR-1:** What puts a startup/founder in the Archive: `is_active = false`? Graduated? Manual action by an admin?
- **Q-AR-2:** What does the Archive page look like? The sheet is empty. Is it the same as the lists, with tabs for Startups and Founders?
- **Q-AR-3:** Are archived startups/founders still visible on their detail pages?

### 11.8 News / Update feed
- **Q-NEWS-1:** Are "Update feed", "News Feed", and "News" the same feature?
- **Q-NEWS-2:** Is "main news" / "thumbnail" a featured top news item chosen with `News.is_primary` (added 2026-10-24)? Can only one news item be primary at a time?
- **Q-NEWS-3:** Who creates news: admins only, or founders too (startup updates)?
- ~~**Q-NEWS-4:** "Filter by tags" for news: what tags?~~ **Answered (2026-10-24):** a separate `Tag` table, linked through `News_Tag`.
- **Q-NEWS-5:** Is there a news detail page (`/News/{id}`)? It isn't in Routes.
- **Q-NEWS-6:** Page size for news pagination?
- **Q-NEWS-7:** `News.is_active` was replaced by `is_primary` on 2026-10-24. Was removing `is_active` intended? Without it, how is a news item hidden or archived?

### 11.9 Booking
- ~~**Q-BOOK-1:** Please confirm: only founders can book?~~ **Answered (v0.5):** every user is a founder, so any logged-in user can book. Visitors are sent to login.
- **Q-BOOK-2:** Who can be booked? **Partly answered (v0.5):** mentors (`is_mentor = true`), who are the same people as crew. Investors are still open (Q-INV-1).
- **Q-BOOK-3:** There is no booking table. What data does a booking need (who, with whom, date/time, duration, topic/notes, status)?
- **Q-BOOK-4:** How is availability defined? Does each mentor set time slots, working hours, or something else?
- **Q-BOOK-5:** What do the states mean exactly: inactive/active, busy/available ("busy until …", "available until …"), and "contact crew"?
- **Q-BOOK-6:** Do bookings need approval from the mentor, or are they confirmed instantly?
- **Q-BOOK-7:** Can bookings be cancelled or rescheduled, and by whom?
- **Q-BOOK-8:** Are sessions online or in person? Should a meeting link or location be stored?
- **Q-BOOK-9:** Should emails or notifications be sent on booking, cancellation, or reminders?
- **Q-BOOK-10:** Should calendar sync (Google/Outlook) be supported?
- **Q-BOOK-11:** What text goes in "Session Guidelines"?
- **Q-BOOK-12:** Limits: maximum bookings per founder per week, session length, how far in advance?
- **Q-BOOK-13:** "Booking with Crew team (Tutorials)": what does "Tutorials" mean here?
- **Q-BOOK-14:** Mentor booking page (WIP): what does it show (incoming bookings, availability settings, accept/decline), and what is its route?
- **Q-BOOK-15:** Can a mentor book a session with another mentor?

### 11.10 Founders Toolkit
- **Q-TK-1:** What is a "Founder Pathway": steps, stages, text content? Who manages it?
- **Q-TK-2:** What are "Requirements": content pages, checklist items, documents? There is no table for them.
- **Q-TK-3:** Video Tutorials "(Udemy)": are they embedded Udemy links, uploaded videos, or both? What does `address_key` hold for them?
- **Q-TK-4:** Are Toolkit Documents downloadable files? Public or login-only?
- **Q-TK-5:** Glossary: why does it have `address_key`? Is it meant to be removed?
- **Q-TK-6:** Checklist: it is per user (`User_Checklist`). The Excel says "mentor cannot checklist", but since v0.5 a mentor is also a normal user (founder). Does that rule still apply, and what happens to checklist progress when a user becomes a mentor? Are checklist items the same for everyone?
- **Q-TK-7:** Can a checklist item be un-completed?
- **Q-TK-8:** Is there a separate checklist page route ("View Your Checklist Page")? It isn't in Routes.
- **Q-TK-9:** Who manages toolkit content: admins only?

### 11.11 Crew / Mentors / About Us
- ~~**Q-CREW-1:** Are "Crew", "Team", and "Mentors" the same people?~~ **Answered (v0.5):** yes, users with `is_mentor = true`.
- **Q-CREW-2:** Is `/AboutUs/Team` a separate page, or a section of `/AboutUs`?
- **Q-CREW-3:** **Partly answered (v0.5):** "mentor" became `User.is_mentor`, and "experience" became the `User Experience` table. Still open: does "links" need its own field, or is `User Contact` enough?
- ~~**Q-CREW-4:** Should crew members have contact info or links like users do?~~ **Answered (v0.5):** mentors are users, so they have `User Contact` and `User Photo`.
- **Q-CREW-5:** Which mentors appear in "Meet the Crew" on the Landing page: all, or selected ones?

### 11.12 Investors (Optional)
- **Q-INV-1:** Are Investors in scope now or later? If in scope: are they users or admin-managed profiles, and what can they do (view startups, be booked)?

### 11.13 Profile and Settings
- **Q-PR-1:** What is on the Profile page: edit own info/photo/contacts/experience? ("my startups" is a separate page since 2026-10-24.)
- **Q-PR-2:** Are startups created and edited from the My Startups page (`/MyStartups`)? The wireframe only shows the list.
- **Q-PR-3:** What is on the Settings page: change password, email, delete account, notifications?
- **Q-PR-4:** Do mentors manage availability and bookings from the mentor booking page (Q-BOOK-14) or from Profile?

### 11.14 Data model
- **Q-DATA-1:** Are all columns not marked "nullable" required?
- **Q-DATA-2:** What exactly is `address_key`: a file path, a cloud storage key (S3/Azure Blob), or a URL? Where will files be stored?
- **Q-DATA-3:** Should `email` be unique on User? (The Crew Member table was removed in v0.5.)
- **Q-DATA-4:** "Tags" vs "Category". **Partly answered (2026-10-24):** they are separate tables. `Tag` is for News and `Category` is for Startups. Startup tags are still open (Q-SU-14).
- **Q-DATA-5:** Should deletes be soft (via `is_active`) or hard? Tables like Startup Photo have no `is_active`.
- **Q-DATA-6:** **Mostly answered (2026-10-24):** Toolkit Glossary and Category `date_modified` are nullable, and User Experience `title`/`description` are nullable. Still open: should `User Experience.date_modified` be nullable like everywhere else?
- **Q-DATA-7:** Primary key type: int auto-increment or GUID?
- **Q-DATA-8:** Do you want the database naming in snake_case, as in the sheet (`user_id`), in PostgreSQL?

### 11.15 Privacy and data
- **Q-PRIV-1:** Who writes the Privacy Policy text?
- **Q-PRIV-2:** Is there any data retention rule (e.g. delete inactive accounts after X)?
- **Q-PRIV-3:** Can users delete their account and data?
