CREATE USER IF NOT EXISTS 'schemax_mobile'@'%' IDENTIFIED BY 'CHANGE_ME';
CREATE USER IF NOT EXISTS 'schemax_mobile'@'localhost' IDENTIFIED BY 'CHANGE_ME';

GRANT SELECT, INSERT, UPDATE, DELETE ON schemax_sis.* TO 'schemax_mobile'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON schemax_sis.* TO 'schemax_mobile'@'localhost';
FLUSH PRIVILEGES;