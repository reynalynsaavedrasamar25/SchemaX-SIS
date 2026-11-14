-- =====================================================================
-- SchemaX capstone seed data (NON-DESTRUCTIVE)
-- Adds 30 students under user_id = 1 (the demo user).
-- Safe to run more than once: existing student_id rows are updated, not duplicated.
-- Run this AFTER schemax_sis.sql has created the tables.
-- =====================================================================
USE schemax_sis;

INSERT INTO students
  (student_id, user_id, first_name, middle_name, surname, gender,
   year_level, program, bloc, birthday, bu_email, contact_no)
VALUES
  ('2026-01-10101', 1, 'Juan', 'Santos', 'Dela Cruz', 'Male', 1, 'BSCS', '1A', '2007-03-14', 'juan.delacruz26@bicol-u.edu.ph', '09171230001'),
  ('2026-01-10102', 1, 'Maria', 'Lopez', 'Reyes', 'Female', 1, 'BSCS', '1A', '2007-07-22', 'maria.reyes26@bicol-u.edu.ph', '09171230002'),
  ('2026-01-10103', 1, 'Carlo', 'Mendoza', 'Garcia', 'Male', 1, 'BSIT', '1B', '2007-01-30', 'carlo.garcia26@bicol-u.edu.ph', '09171230003'),
  ('2026-01-10104', 1, 'Angela', 'Ramos', 'Torres', 'Female', 1, 'BSIT', '1B', '2007-09-05', 'angela.torres26@bicol-u.edu.ph', '09171230004'),
  ('2026-01-10105', 1, 'Miguel', 'Aquino', 'Bautista', 'Male', 1, 'BSCS', '1B', '2007-05-18', 'miguel.bautista26@bicol-u.edu.ph', '09171230005'),
  ('2026-01-10106', 1, 'Patricia', 'Flores', 'Villanueva', 'Female', 1, 'BSIT', '1A', '2007-11-02', 'patricia.villanueva26@bicol-u.edu.ph', '09171230006'),
  ('2026-01-10107', 1, 'Joshua', 'Castillo', 'Navarro', 'Male', 1, 'BSIT', '1A', '2007-02-27', 'joshua.navarro26@bicol-u.edu.ph', '09171230007'),
  ('2026-01-10108', 1, 'Kristine', 'Domingo', 'Pascual', 'Female', 1, 'BSCS', '1A', '2007-08-11', 'kristine.pascual26@bicol-u.edu.ph', '09171230008'),
  ('2025-01-10201', 1, 'Mark', 'Rivera', 'Soriano', 'Male', 2, 'BSCS', '2A', '2006-04-09', 'mark.soriano25@bicol-u.edu.ph', '09171230009'),
  ('2025-01-10202', 1, 'Jasmine', 'Cruz', 'Ocampo', 'Female', 2, 'BSIT', '2A', '2006-06-21', 'jasmine.ocampo25@bicol-u.edu.ph', '09171230010'),
  ('2025-01-10203', 1, 'Daniel', 'Padilla', 'Mercado', 'Male', 2, 'BSIT', '2B', '2006-12-15', 'daniel.mercado25@bicol-u.edu.ph', '09171230011'),
  ('2025-01-10204', 1, 'Nicole', 'Salazar', 'Estrada', 'Female', 2, 'BSCS', '2A', '2006-03-03', 'nicole.estrada25@bicol-u.edu.ph', '09171230012'),
  ('2025-01-10205', 1, 'Paolo', 'Guerrero', 'Manalo', 'Male', 2, 'BSIT', '2B', '2006-09-28', 'paolo.manalo25@bicol-u.edu.ph', '09171230013'),
  ('2025-01-10206', 1, 'Bea', 'Fernandez', 'Cabrera', 'Female', 2, 'BSCS', '2B', '2006-01-19', 'bea.cabrera25@bicol-u.edu.ph', '09171230014'),
  ('2025-01-10207', 1, 'Ethan', 'Valdez', 'Lim', 'Male', 2, 'BSIT', '2A', '2006-07-07', 'ethan.lim25@bicol-u.edu.ph', '09171230015'),
  ('2025-01-10208', 1, 'Camille', 'Tan', 'Rosales', 'Female', 2, 'BSCS', '2B', '2006-05-25', 'camille.rosales25@bicol-u.edu.ph', '09171230016'),
  ('2024-01-10301', 1, 'Rafael', 'Morales', 'Hernandez', 'Male', 3, 'BSCS', '3A', '2005-02-12', 'rafael.hernandez24@bicol-u.edu.ph', '09171230017'),
  ('2024-01-10302', 1, 'Sofia', 'Delos Santos', 'Gomez', 'Female', 3, 'BSIT', '3A', '2005-10-30', 'sofia.gomez24@bicol-u.edu.ph', '09171230018'),
  ('2024-01-10303', 1, 'Gabriel', 'Pineda', 'Aguilar', 'Male', 3, 'BSCS', '3B', '2005-06-16', 'gabriel.aguilar24@bicol-u.edu.ph', '09171230019'),
  ('2024-01-10304', 1, 'Isabel', 'Santiago', 'Evangelista', 'Female', 3, 'BSIT', '3B', '2005-04-01', 'isabel.evangelista24@bicol-u.edu.ph', '09171230020'),
  ('2024-01-10305', 1, 'Lorenzo', 'Cortez', 'Robles', 'Male', 3, 'BSIT', '3A', '2005-12-08', 'lorenzo.robles24@bicol-u.edu.ph', '09171230021'),
  ('2024-01-10306', 1, 'Andrea', 'Javier', 'Macaraeg', 'Female', 3, 'BSCS', '3A', '2005-08-23', 'andrea.macaraeg24@bicol-u.edu.ph', '09171230022'),
  ('2024-01-10307', 1, 'Vincent', 'Tolentino', 'Ibarra', 'Male', 3, 'BSCS', '3B', '2005-03-17', 'vincent.ibarra24@bicol-u.edu.ph', '09171230023'),
  ('2024-01-10308', 1, 'Hannah', 'Bernardo', 'Lacson', 'Female', 3, 'BSIT', '3A', '2005-11-26', 'hannah.lacson24@bicol-u.edu.ph', '09171230024'),
  ('2023-01-10401', 1, 'Anthony', 'Gonzales', 'Perez', 'Male', 4, 'BSCS', '4A', '2004-05-04', 'anthony.perez23@bicol-u.edu.ph', '09171230025'),
  ('2023-01-10402', 1, 'Michelle', 'Rosario', 'Alvarez', 'Female', 4, 'BSIT', '4A', '2004-09-13', 'michelle.alvarez23@bicol-u.edu.ph', '09171230026'),
  ('2023-01-10403', 1, 'Francis', 'Dizon', 'Velasco', 'Male', 4, 'BSCS', '4A', '2004-01-29', 'francis.velasco23@bicol-u.edu.ph', '09171230027'),
  ('2023-01-10404', 1, 'Trisha', 'Malabanan', 'Abad', 'Female', 4, 'BSIT', '4B', '2004-07-20', 'trisha.abad23@bicol-u.edu.ph', '09171230028'),
  ('2023-01-10405', 1, 'Jericho', 'Soriano', 'Dela Rosa', 'Male', 4, 'BSCS', '4B', '2004-10-10', 'jericho.delarosa23@bicol-u.edu.ph', '09171230029'),
  ('2023-01-10406', 1, 'Lara', 'Espiritu', 'Medina', 'Female', 4, 'BSIT', '4A', '2004-02-06', 'lara.medina23@bicol-u.edu.ph', '09171230030')
ON DUPLICATE KEY UPDATE
  first_name  = VALUES(first_name),
  middle_name = VALUES(middle_name),
  surname     = VALUES(surname),
  gender      = VALUES(gender),
  year_level  = VALUES(year_level),
  program     = VALUES(program),
  bloc        = VALUES(bloc),
  birthday    = VALUES(birthday),
  bu_email    = VALUES(bu_email),
  contact_no  = VALUES(contact_no);

-- Quick check: should return 32 (30 new + the 2 from the midterm)
SELECT COUNT(*) AS total_students FROM students WHERE user_id = 1;