# FUNews Management System - PRN222 Assignment 02

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Razor Pages](https://img.shields.io/badge/ASP.NET%20Core-Razor%20Pages-0D6EFD?style=for-the-badge)
![SignalR](https://img.shields.io/badge/SignalR-Real--Time-FF6F00?style=for-the-badge)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-6DB33F?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-FUNewsManagement-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

## Student Information

| Field | Value |
| --- | --- |
| Student | Pham Hong Phuc |
| Class | SE19B.NET |
| Student ID | QE190133 |
| Assignment | PRN222 Assignment 02 |
| Project Type | ASP.NET Core Razor Pages |

## Project Overview

`FUNews Management System` is an ASP.NET Core Razor Pages application for managing university news. The system supports public active-news viewing, authenticated member features, admin account/report management, staff category/news/profile management, lecturer active-news viewing, live search, and SignalR real-time updates.

The project follows the required layered architecture:

```text
Razor PageModel -> Service -> Repository -> DAO -> DbContext -> SQL Server
```

Razor PageModels do not access `FunewsDbContext` directly.

## Solution Structure

```text
PhamHongPhuc_SE19B.NET_A02.sln
|
|-- PhamHongPhucRazorPages
|   |-- Pages
|   |-- Hubs
|   |-- wwwroot
|   |-- Program.cs
|   `-- appsettings.json
|
|-- BusinessObjects
|   `-- Models
|
|-- DataAccessObjects
|   |-- Data
|   `-- DAO classes
|
|-- Repositories
|   |-- Interfaces
|   `-- Implementations
|
`-- Services
    |-- Interfaces
    `-- Implementations
```

## Technology Stack

| Area | Technology |
| --- | --- |
| Framework | .NET 8 |
| Web UI | ASP.NET Core Razor Pages |
| Database | SQL Server |
| ORM | Entity Framework Core 8 |
| Real-time | SignalR |
| Query | LINQ |
| Architecture | 3-layer architecture |
| Patterns | Repository Pattern, Singleton Pattern |
| UI | Bootstrap 5, Bootstrap Icons, custom CSS |
| Authentication | Cookie authentication |

## Account And Roles

| Role | Source | Permission |
| --- | --- | --- |
| Admin | `appsettings.json` | Account management, report statistics |
| Staff | Database role `1` | Category, news article, profile, news history |
| Lecturer | Database role `2` | View active news only |

Default admin account:

```text
Email: admin@FUNewsManagementSystem.org
Password: @@abc123@@
```

Configured in:

```json
"AdminAccount": {
  "Email": "admin@FUNewsManagementSystem.org",
  "Password": "@@abc123@@"
}
```

## Database Configuration

Database name:

```text
FUNewsManagement
```

Connection string file:

```text
PhamHongPhucRazorPages/appsettings.json
```

Default connection:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FUNewsManagement;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

If your SQL Server instance is different, update `Server=localhost\\SQLEXPRESS`.

## Feature Map

| Feature | Admin | Staff | Lecturer | Public |
| --- | ---: | ---: | ---: | ---: |
| View active news | Yes | Yes | Yes | Yes |
| Public live search | Yes | Yes | Yes | Yes |
| Manage accounts | Yes | No | No | No |
| Account live search | Yes | No | No | No |
| Report statistics | Yes | No | No | No |
| Manage categories | No | Yes | No | No |
| Manage news articles | No | Yes | No | No |
| News article live search | No | Yes | No | No |
| SignalR news updates | No | Yes | No | No |
| Manage own profile | No | Yes | No | No |
| View own news history | No | Yes | No | No |

## Main Pages

| Route | Description |
| --- | --- |
| `/Login` | Login page |
| `/Logout` | Logout page |
| `/AccessDenied` | Access denied page |
| `/` | Public active news |
| `/News/PublicIndex` | Public active news page |
| `/Admin/Accounts/Index` | Admin account management |
| `/Admin/Reports/Index` | Admin report statistics |
| `/Staff/Categories/Index` | Staff category management |
| `/Staff/NewsArticles/Index` | Staff news article management |
| `/Staff/Profile/Index` | Staff profile management |
| `/Staff/NewsHistory/Index` | Staff news history |
| `/Lecturer/News/Index` | Lecturer active news list |

## SignalR Real-Time News

SignalR hub:

```text
/newsHub
```

News article operations notify all connected clients:

```text
ReceiveNewsUpdate
```

When a staff user creates, updates, or deletes a news article, the news article table can refresh automatically. If a search keyword is active, the table reloads with the same keyword.

## Live Search

| Page | Search Fields |
| --- | --- |
| Public News | Title, headline, category, tag |
| Staff News Articles | Title, headline, category, tag, status |
| Admin Accounts | Name, email, role |

Live search uses a debounce delay of about `300ms` to avoid sending too many requests.

## UI Highlights

- Bootstrap 5 dashboard-style layout.
- Sidebar and top navbar for authenticated pages.
- Login page with professional centered card.
- Public news cards with category badge, created date, author, and search.
- Bootstrap cards for statistics.
- Tables, badges, alerts, buttons, and validation messages.
- Modal popup for create/update actions.
- Confirmation modal for delete actions.
- Custom CSS file:

```text
PhamHongPhucRazorPages/wwwroot/css/site-custom.css
```

## How To Run

1. Create SQL Server database `FUNewsManagement` using the assignment SQL script.
2. Check `PhamHongPhucRazorPages/appsettings.json`.
3. Build the solution:

```powershell
dotnet build
```

4. Run the Razor Pages project:

```powershell
dotnet run --project PhamHongPhucRazorPages
```

5. Open the localhost URL shown in the terminal.

## Testing Checklist

| Test Case | Expected Result |
| --- | --- |
| Public news without login | Show active news only |
| Public live search | News cards update while typing |
| Login admin account | Show admin features |
| Admin account live search | Account table updates while typing |
| Account CRUD | Create/update/delete/search with validation |
| Admin report | Filter by date and sort by created date descending |
| Login staff account | Show staff management features |
| Category CRUD | Create/update/delete/search with validation |
| Delete used category | Prevent delete and show error |
| News CRUD with tags | Save article and tags correctly |
| News SignalR update | Other clients receive `ReceiveNewsUpdate` |
| News live search | Table updates while typing and preserves keyword on SignalR refresh |
| Staff profile | Staff can update only own profile |
| Staff news history | Show only current staff's created news |
| Lecturer login | Show active news only |
| Unauthorized access | Redirect to Login or AccessDenied |

## Build Verification

Use this command before submission:

```powershell
dotnet build
```

Expected result:

```text
Build succeeded.
0 Error(s)
```

