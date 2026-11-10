# SchemaX – Integrated Student Information & Mobile Analytics Ecosystem

CS 106 Final Capstone · Group 3 – SchemaX

SchemaX extends our midterm WinForms Student Information System (SIS) into three connected parts that share **one MySQL database**:

| Part | Technology | What it does |
| --- | --- | --- |
| Desktop app | C#, .NET 10 Windows Forms, ScottPlot | Student management plus a live analytics dashboard with a Refresh Dashboard action |
| Mobile app | .NET MAUI (Android), MySqlConnector, async/await | Student directory with search, filters, add/edit and login |
| Packages | Desktop installer (.msi/.exe) and signed .apk | Install both apps on a clean machine |

Documentation is kept in a five-node CherryTree notebook at 'docs/SchemaX-SIS.ctd'.

## Repository

https://github.com/reynalynsaavedrasamar25/SchemaX-SIS

Midterm version: tag 'v1.0-midterm'. Capstone work is merged into 'develop', then released to 'main' as 'v1.0'.

## Team and roles

| Member | Capstone role | Main branch |
| --- | --- | --- |
| Reynalyn Samar | Desktop ScottPlot Analytics & Dashboard | 'feature/desktop-scottplot' |
| Zedric Zuñiga | .NET MAUI Mobile UI & Navigation | 'feature/mobile-ui' |
| Marianne Nuarin | Async MySQL Data Sync & Loopback Routing (10.0.2.2) | 'feature/mobile-data', 'feature/db-script' |
| Princess Ida Camasis | Installer Packaging (.msi & .apk) & CherryTree Documentation | 'feature/installers', 'feature/docs' |

Git (Criterion 4) and the live demo (Criterion 6) are shared by all four members.

## Repository layout

'''
SchemaX-SIS/
├─ SchemaX.SIS/            WinForms desktop app and the solution file (SchemaX.SIS.slnx)
├─ SchemaX.Mobile/         .NET MAUI Android app (capstone)
├─ SchemaX.ValidatorTests/ xUnit tests
├─ db/                     SQL scripts and database notes
├─ installer/              Installer script and build scripts (capstone)
├─ docs/                   CherryTree notebook, screenshots, contrib/ notes
└─ README.md, README.txt
'''

## Requirements

- Visual Studio 2026 with the .NET desktop and .NET MAUI workloads
- .NET 10 SDK
- MySQL Server 8.x
- Android emulator (Android 10 / API 29 or higher)

Clone the repository into a folder that is **not** synced by OneDrive, for example 'C:\Projects\SchemaX-SIS'. Cloud-sync tools can lock the 'bin' and 'obj' folders and cause build errors.

## Getting started

1. Clone and switch to 'develop':
   '''
   git clone https://github.com/reynalynsaavedrasamar25/SchemaX-SIS.git
   cd SchemaX-SIS
   git checkout develop
   '''
2. Create the database: 'mysql -u root -p < db/schemax_sis.sql'
   (this script drops and recreates the tables – do not run it on real data).
3. Copy 'appsettings.example.json' to 'appsettings.json' in the same folder and enter your own MySQL password. **Never commit 'appsettings.json'.**
4. Open 'SchemaX.SIS/SchemaX.SIS.slnx' in Visual Studio and build.

On the Android emulator the host PC is reached at '10.0.2.2', not 'localhost'. The desktop app keeps using 'localhost`.

## Git workflow

- 'main' is releasable. 'develop' is the integration branch. Work happens in 'feature/<area>' branches created from 'develop'.
- Every change goes through a **pull request into 'develop'** with at least one teammate's approval. Keep pull requests small, merge, then delete the branch.
- Start each work session with 'git pull --rebase origin develop'.
- The release is a pull request from 'develop' into 'main', tagged 'v1.0'.

### Commit message prefixes

| Prefix | Use for |
| --- | --- |
| 'feat:' | New behaviour |
| 'fix:' | Bug fix |
| 'docs:' | Documentation and notebook content |
| 'test:' | Tests |
| 'chore:' | Configuration, packaging, housekeeping |
| 'refactor:' | Restructuring without behaviour change |

### Files with a single owner (to avoid merge conflicts)

| Files | Owner |
| --- | --- |
| 'dashboardForm.cs', '.Designer.cs', '.resx' | Reynalyn |
| 'docs/SchemaX-SIS.ctd', '.gitignore', 'installer/*' | Princess Ida |
| 'db/*.sql' | Marianne |
| Mobile 'Colors.xaml', 'AppShell.xaml' | Zedric |

Other members write their documentation in 'docs/contrib/<name>.md', and Princess Ida merges it into the notebook.

## Security rules

Do not commit passwords, 'appsettings.json', '.keystore' or '.jks' files, or built packages ('.apk', '.aab', '.msi'). Commit 'appsettings.example.json' with placeholder values instead.

## Midterm features (carried over)

Registration and login, student CRUD with validation, search and filtering, enrolment and GWA calculation, error handling with logging, and xUnit tests for validation and GWA.

## Capstone deliverables

1. Demo video (3–5 minutes)
2. Visual Studio solution with both projects
3. Updated database script
4. Desktop installer and signed '.apk'
5. CherryTree documentation
6. `README.txt` with members, task matrix and repository link

