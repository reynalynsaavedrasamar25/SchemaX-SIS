# Database

schemax_sis.sql creates the `schemax_sis` database with four tables:
students, subjects, student_subjects (enrollment and grades) and attendance.

## Import
mysql -u root -p < db/schemax_sis.sql

WARNING: the script starts with DROP TABLE statements. Running it again deletes all existing
data in these tables. Do not run it on a database that holds real records.

Deleting a student also deletes their student_subjects and attendance rows (ON DELETE CASCADE).

## Run order (important)

Run the scripts in Workbench with the **root** account, in this order:

1. `schemax_sis.sql`: creates the database and tables, plus the demo user and 2 students.
   **Warning:** it drops and recreates all tables, so run it first and only once.
2. `02_capstone_seed.sql`: adds 30 sample students under the demo user. Safe to re-run.
3. `03_mobile_user.sql`: creates the `schemax_mobile` MySQL account used by the Android app.
   Replace `CHANGE_ME` with a real password in Workbench only, and never commit the real password.

After step 2, this should return 32:

    SELECT COUNT(*) FROM schemax_sis.students;

The mobile app connects through the Android emulator address `10.0.2.2` (the host PC).