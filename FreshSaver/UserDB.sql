CREATE DATABASE IF NOT EXISTS UserDB;
USE UserDB;
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL
);

INSERT INTO users (username, email, password)
VALUES ('exampleUser', 'user@example.com', 'password123');

SELECT * FROM users;

SELECT * FROM users WHERE username = 'exampleUser';

UPDATE users
SET email = 'newemail@example.com'
WHERE username = 'exampleUser';

DELETE FROM users WHERE username = 'exampleUser';