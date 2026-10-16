create database if not exists schemax_sis
character set utf8mb4 collate utf8mb4_unicode_ci;
use schemax_sis;

drop table if exists attendance;
drop table if exists student_subjects;
drop table if exists subjects;
drop table if exists students;

create table students (
student_id varchar(15) not null,
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
constraint chk_year_level check (year_level between 1 and 5)
) engine=InnoDB;

create table subjects (
subject_id int auto_increment primary key,
subject_code varchar(15) not null unique,
title varchar(100) not null,
units tinyint not null
)engine=InnoDB;

create table student_subjects (
id int auto_increment primary key,
student_id varchar(15) not null,
subject_id int not null,
grade decimal(3,2) null,
unique key uq_student_subject (student_id, subject_id),
foreign key (student_id) references students(student_id) on delete cascade,
foreign key (subject_id) references subjects(subject_id)
)engine=InnoDB;

create table attendance (
attendance_id int auto_increment primary key,
student_id varchar(15) not null,
subject_id int not null,
attendance_date date not null,
status enum('Present', 'Absent', 'Late', 'Excused') not null,
unique key uq_attendance (student_id, subject_id, attendance_date),
foreign key (student_id) references students(student_id) on delete cascade,
foreign key (subject_id) references subjects(subject_id)
)engine=InnoDB;

insert into subjects (subject_code, title,  units) values
('CS106', 'Applications Development and Emerging Technologies', 3),
('CS104', 'Object-Oriented Programming', 3),
('CS108', 'Data Structures and Algorithms', 3),
('GEC14', 'Mathematics in Modern World', 3),
('MathElec1', 'Linear Algebra', 3);

insert into students
(student_id, first_name, middle_name, surname, gender, year_level, program, bloc,
birthday, bu_email, contact_no) values
('2025-06969', 'Reynalyn', 'Saavedra', 'Samar', 'Female',2,'BSCS', '2B', '2006-10-06', 
'reynalynsaavedra.samar25@bicol-u.edu.ph', '09762662951'),
('2025-06767', 'Mariane', 'Nonsol', 'Nuarin', 'Female',2,'BSCS', '2B', '2006-11-09', 
'marianenonsol.nuarin25@bicol-u.edu.ph', '09335467892');

insert into student_subjects (student_id, subject_id, grade) values
('2025-06969', 1, 2.00), ('2025-06969', 2, 1.50), ('2025-06969', 3, NULL);
