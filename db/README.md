
# Database

The `db/schemax_sis.sql` script creates the `schemax_sis` database with five tables:

- `users` — User accounts and authentication
- `students` — Student information linked to user accounts
- `subjects` — Subject information
- `student_subjects` — Student enrollments and grades
- `attendance` — Student attendance records

## Import

```bash
mysql -u root -p < db/schemax_sis.sql
```

**WARNING:** The SQL script contains `DROP TABLE` statements. Running it will delete existing data in these tables. Back up your database before importing.

## Database Relationships

- Each student is linked to a user through `students.user_id`.
- Student enrollments and grades are stored in `student_subjects`.
- Attendance records are stored in `attendance`.
- Deleting a student also deletes their related enrollment and attendance records through `ON DELETE CASCADE`.

## Validation

- Student year level: 1–5
- Subject units: 1–6
- Grade: 1.00–5.00, or NULL for ungraded subjects
