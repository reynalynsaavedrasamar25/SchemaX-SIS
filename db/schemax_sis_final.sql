-- =====================================================================
-- SchemaX SIS - FINAL DATABASE SCRIPT (schema + seed data + mobile user)
--
-- WARNING: DESTRUCTIVE. This script DROPS and recreates every table in
-- schemax_sis. Do NOT run it on a database that holds real data.
-- Run it in MySQL Workbench with the ROOT account, on a clean server.
--
-- Demo login for BOTH apps:   username: reynasaavedra   password: Demo12345
-- (The password hash below is SHA-256 in hex text, the same format the
--  desktop PasswordHasher uses and the mobile login checks.)
--
-- The Android app uses a limited MySQL account, schemax_mobile.
-- Its password below is a placeholder. After running the script, set the
-- real one in Workbench only, and never commit it:
--   ALTER USER 'schemax_mobile'@'%'         IDENTIFIED BY '<real password>';
--   ALTER USER 'schemax_mobile'@'localhost' IDENTIFIED BY '<real password>';
-- =====================================================================

create database if not exists schemax_sis
character set utf8mb4 collate utf8mb4_unicode_ci;
use schemax_sis;

drop table if exists attendance;
drop table if exists student_subjects;
drop table if exists subjects;
drop table if exists students;
drop table if exists users;

-- ---------------------------------------------------------------------
-- Tables
-- ---------------------------------------------------------------------
create table users (
user_id int auto_increment primary key,
first_name varchar(50) not null,
surname varchar(50) not null,
bu_email varchar(100) not null,
username varchar(50) not null,
password_hash varchar(255) not null,
created_at timestamp not null default current_timestamp,
unique key uq_users_username (username),
unique key uq_users_email (bu_email)
) engine=InnoDB;

create table students (
student_id varchar(15) not null,
user_id int not null,
first_name varchar(50) not null,
middle_name varchar(50) null,
surname varchar(50) not null,
gender enum('Male', 'Female', 'Other') not null,
year_level tinyint not null,
program varchar(50) not null,
bloc varchar(10) not null,
birthday date not null,
bu_email varchar(100) not null,
contact_no varchar(20) not null,
photo_path varchar(300) null,
created_at timestamp not null default current_timestamp,
primary key (student_id),
unique key uq_students_email (bu_email),
index idx_students_user_name (user_id, surname, first_name),
constraint fk_students_user foreign key (user_id)
references users(user_id) on delete cascade,
constraint chk_year_level check (year_level between 1 and 5)
) engine=InnoDB;

create table subjects (
subject_id int auto_increment primary key,
subject_code varchar(15) not null unique,
title varchar(100) not null,
units tinyint not null,
constraint chk_units check (units between 1 and 6)
) engine=InnoDB;

create table student_subjects (
id int auto_increment primary key,
student_id varchar(15) not null,
subject_id int not null,
grade decimal(3,2) null,
unique key uq_student_subject (student_id, subject_id),
foreign key (student_id) references students(student_id) on delete cascade,
foreign key (subject_id) references subjects(subject_id),
constraint chk_grade check (grade between 1.00 and 5.00)
) engine=InnoDB;

create table attendance (
attendance_id int auto_increment primary key,
student_id varchar(15) not null,
subject_id int not null,
attendance_date date not null,
status enum('Present', 'Absent', 'Late', 'Excused') not null,
unique key uq_attendance (student_id, subject_id, attendance_date),
foreign key (student_id) references students(student_id) on delete cascade,
foreign key (subject_id) references subjects(subject_id)
) engine=InnoDB;

-- ---------------------------------------------------------------------
-- Seed data
-- ---------------------------------------------------------------------
insert into subjects (subject_code, title, units) values
('CS106', 'Applications Development and Emerging Technologies', 3),
('CS104', 'Object-Oriented Programming', 3),
('CS108', 'Data Structures and Algorithms', 3),
('GEC14', 'Mathematics in Modern World', 3),
('MathElec1', 'Linear Algebra', 3);

-- Demo user. Password: Demo12345 (SHA-256, hex text)
insert into users (first_name, surname, bu_email, username, password_hash) values
('Reyna', 'Saavedra', 'reynasamar.saavedra25@bicol-u.edu.ph', 'reynasaavedra',
 upper(sha2('Demo12345', 256)));

set @demo_id = (select user_id from users where username = 'reynasaavedra');

insert into students
(student_id, user_id, first_name, middle_name, surname, gender, year_level, program, bloc,
birthday, bu_email, contact_no) values
('2025-01-06969', @demo_id, 'Reynalyn', 'Saavedra', 'Samar', 'Female', 2, 'BSCS', '2B', '2006-10-06', 'reynalynsaavedra.samar25@bicol-u.edu.ph', '09762662951'),
('2025-01-06767', @demo_id, 'Mariane', 'Nonsol', 'Nuarin', 'Female', 2, 'BSCS', '2B', '2006-11-09', 'marianenonsol.nuarin25@bicol-u.edu.ph', '09335467892'),
('2026-01-10101', @demo_id, 'Juan', 'Santos', 'Dela Cruz', 'Male', 1, 'BSCS', '1A', '2007-03-14', 'juan.delacruz26@bicol-u.edu.ph', '09171230001'),
('2026-01-10102', @demo_id, 'Maria', 'Lopez', 'Reyes', 'Female', 1, 'BSCS', '1A', '2007-07-22', 'maria.reyes26@bicol-u.edu.ph', '09171230002'),
('2026-01-10103', @demo_id, 'Carlo', 'Mendoza', 'Garcia', 'Male', 1, 'BSIT', '1B', '2007-01-30', 'carlo.garcia26@bicol-u.edu.ph', '09171230003'),
('2026-01-10104', @demo_id, 'Angela', 'Ramos', 'Torres', 'Female', 1, 'BSIT', '1B', '2007-09-05', 'angela.torres26@bicol-u.edu.ph', '09171230004'),
('2026-01-10105', @demo_id, 'Miguel', 'Aquino', 'Bautista', 'Male', 1, 'BSCS', '1B', '2007-05-18', 'miguel.bautista26@bicol-u.edu.ph', '09171230005'),
('2026-01-10106', @demo_id, 'Patricia', 'Flores', 'Villanueva', 'Female', 1, 'BSIT', '1A', '2007-11-02', 'patricia.villanueva26@bicol-u.edu.ph', '09171230006'),
('2026-01-10107', @demo_id, 'Joshua', 'Castillo', 'Navarro', 'Male', 1, 'BSIT', '1A', '2007-02-27', 'joshua.navarro26@bicol-u.edu.ph', '09171230007'),
('2026-01-10108', @demo_id, 'Kristine', 'Domingo', 'Pascual', 'Female', 1, 'BSCS', '1A', '2007-08-11', 'kristine.pascual26@bicol-u.edu.ph', '09171230008'),
('2025-01-10201', @demo_id, 'Mark', 'Rivera', 'Soriano', 'Male', 2, 'BSCS', '2A', '2006-04-09', 'mark.soriano25@bicol-u.edu.ph', '09171230009'),
('2025-01-10202', @demo_id, 'Jasmine', 'Cruz', 'Ocampo', 'Female', 2, 'BSIT', '2A', '2006-06-21', 'jasmine.ocampo25@bicol-u.edu.ph', '09171230010'),
('2025-01-10203', @demo_id, 'Daniel', 'Padilla', 'Mercado', 'Male', 2, 'BSIT', '2B', '2006-12-15', 'daniel.mercado25@bicol-u.edu.ph', '09171230011'),
('2025-01-10204', @demo_id, 'Nicole', 'Salazar', 'Estrada', 'Female', 2, 'BSCS', '2A', '2006-03-03', 'nicole.estrada25@bicol-u.edu.ph', '09171230012'),
('2025-01-10205', @demo_id, 'Paolo', 'Guerrero', 'Manalo', 'Male', 2, 'BSIT', '2B', '2006-09-28', 'paolo.manalo25@bicol-u.edu.ph', '09171230013'),
('2025-01-10206', @demo_id, 'Bea', 'Fernandez', 'Cabrera', 'Female', 2, 'BSCS', '2B', '2006-01-19', 'bea.cabrera25@bicol-u.edu.ph', '09171230014'),
('2025-01-10207', @demo_id, 'Ethan', 'Valdez', 'Lim', 'Male', 2, 'BSIT', '2A', '2006-07-07', 'ethan.lim25@bicol-u.edu.ph', '09171230015'),
('2025-01-10208', @demo_id, 'Camille', 'Tan', 'Rosales', 'Female', 2, 'BSCS', '2B', '2006-05-25', 'camille.rosales25@bicol-u.edu.ph', '09171230016'),
('2024-01-10301', @demo_id, 'Rafael', 'Morales', 'Hernandez', 'Male', 3, 'BSCS', '3A', '2005-02-12', 'rafael.hernandez24@bicol-u.edu.ph', '09171230017'),
('2024-01-10302', @demo_id, 'Sofia', 'Delos Santos', 'Gomez', 'Female', 3, 'BSIT', '3A', '2005-10-30', 'sofia.gomez24@bicol-u.edu.ph', '09171230018'),
('2024-01-10303', @demo_id, 'Gabriel', 'Pineda', 'Aguilar', 'Male', 3, 'BSCS', '3B', '2005-06-16', 'gabriel.aguilar24@bicol-u.edu.ph', '09171230019'),
('2024-01-10304', @demo_id, 'Isabel', 'Santiago', 'Evangelista', 'Female', 3, 'BSIT', '3B', '2005-04-01', 'isabel.evangelista24@bicol-u.edu.ph', '09171230020'),
('2024-01-10305', @demo_id, 'Lorenzo', 'Cortez', 'Robles', 'Male', 3, 'BSIT', '3A', '2005-12-08', 'lorenzo.robles24@bicol-u.edu.ph', '09171230021'),
('2024-01-10306', @demo_id, 'Andrea', 'Javier', 'Macaraeg', 'Female', 3, 'BSCS', '3A', '2005-08-23', 'andrea.macaraeg24@bicol-u.edu.ph', '09171230022'),
('2024-01-10307', @demo_id, 'Vincent', 'Tolentino', 'Ibarra', 'Male', 3, 'BSCS', '3B', '2005-03-17', 'vincent.ibarra24@bicol-u.edu.ph', '09171230023'),
('2024-01-10308', @demo_id, 'Hannah', 'Bernardo', 'Lacson', 'Female', 3, 'BSIT', '3A', '2005-11-26', 'hannah.lacson24@bicol-u.edu.ph', '09171230024'),
('2023-01-10401', @demo_id, 'Anthony', 'Gonzales', 'Perez', 'Male', 4, 'BSCS', '4A', '2004-05-04', 'anthony.perez23@bicol-u.edu.ph', '09171230025'),
('2023-01-10402', @demo_id, 'Michelle', 'Rosario', 'Alvarez', 'Female', 4, 'BSIT', '4A', '2004-09-13', 'michelle.alvarez23@bicol-u.edu.ph', '09171230026'),
('2023-01-10403', @demo_id, 'Francis', 'Dizon', 'Velasco', 'Male', 4, 'BSCS', '4A', '2004-01-29', 'francis.velasco23@bicol-u.edu.ph', '09171230027'),
('2023-01-10404', @demo_id, 'Trisha', 'Malabanan', 'Abad', 'Female', 4, 'BSIT', '4B', '2004-07-20', 'trisha.abad23@bicol-u.edu.ph', '09171230028'),
('2023-01-10405', @demo_id, 'Jericho', 'Soriano', 'Dela Rosa', 'Male', 4, 'BSCS', '4B', '2004-10-10', 'jericho.delarosa23@bicol-u.edu.ph', '09171230029'),
('2023-01-10406', @demo_id, 'Lara', 'Espiritu', 'Medina', 'Female', 4, 'BSIT', '4A', '2004-02-06', 'lara.medina23@bicol-u.edu.ph', '09171230030');

insert into student_subjects (student_id, subject_id, grade) values
('2025-01-06969', 1, 2.00), ('2025-01-06969', 2, 1.50), ('2025-01-06969', 3, NULL);

-- ---------------------------------------------------------------------
-- Limited account for the Android app (placeholder password)
-- ---------------------------------------------------------------------
create user if not exists 'schemax_mobile'@'%' identified by 'CHANGE_ME';
create user if not exists 'schemax_mobile'@'localhost' identified by 'CHANGE_ME';
grant select, insert, update, delete on schemax_sis.* to 'schemax_mobile'@'%';
grant select, insert, update, delete on schemax_sis.* to 'schemax_mobile'@'localhost';
flush privileges;

-- Quick check: students = 32, subjects = 5, users = 1
select (select count(*) from students) as students,
       (select count(*) from subjects) as subjects,
       (select count(*) from users) as users;