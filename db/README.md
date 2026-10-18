# Database

schemax_sis.sql creates the `schemax_sis` database with four tables:
students, subjects, student_subjects (enrollment and grades) and attendance.

## Import
mysql -u root -p < db/schemax_sis.sql

WARNING: the script starts with DROP TABLE statements. Running it again deletes all existing
data in these tables. Do not run it on a database that holds real records.

Deleting a student also deletes their student_subjects and attendance rows (ON DELETE CASCADE).
