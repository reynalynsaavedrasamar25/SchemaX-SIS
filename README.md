# SchemaX SIS

Student Information System
Group 3 - Team SchemaX

## Members
- Zedric Zuñiga: UI Layout and DataGridView Display
- Reynalyn Samar: MySQL Setup & Database Connection
- Mariane Nuarin: CRUD Logic (Add, Edit, Delete)
- Princess Ida Camasis: Error Handling and CherryTree Documentation

## Setup
Requires: .NET 10 SDK, MySQL 8.0.16 or newer (the schema uses CHECK constraints), and
Visual Studio 2022 or newer with the ".NET desktop development" workload.

1. Import db/schemax_sis.sql into MySQL.
   WARNING: the script drops and recreates all tables, so importing it again erases existing data.
2. In SchemaX.SIS/SchemaX.SIS/, copy appsettings.example.json to appsettings.json
   and set your MySQL password. This file is git-ignored; never commit it.
3. Open SchemaX.SIS/SchemaX.SIS.slnx in Visual Studio and press F5.
4. To run the tests: dotnet test SchemaX.ValidatorTests
