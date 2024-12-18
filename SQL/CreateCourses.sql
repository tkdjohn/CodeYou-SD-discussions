USE Courses;
DROP TABLE IF EXISTS Assignment;
DROP TABLE IF EXISTS Course;
DROP TABLE IF EXISTS Student;

GO

CREATE TABLE Course (
    CourseId    INT        NOT NULL PRIMARY KEY,
    CourseName VARCHAR(50) NOT NULL,
    CourseDescripion NVARCHAR(MAX) NULL
)

CREATE TABLE Student (
    StudentId          INT NOT NULL PRIMARY KEY,
    LastName   VARCHAR(20) NOT NULL,
    MiddleName VARCHAR(20) NULL,
    FirstName  VARCHAR(20) NOT NULL
)

CREATE TABLE Assignment (
    AssignmentId   INT         NOT NULL PRIMARY KEY,
    AssignmentName VARCHAR(20) NOT NULL,
    DueDate        DATETIME    NOT NULL,
    SubmitDate     DATETIME    NOT NULL,
    Grade          DECIMAL     NOT NULL,
    CourseId       INT         NOT NULL,
    StudentId      INT         NOT NULL,
    Comment        VARCHAR(200) NULL,
    FOREIGN KEY (CourseId) REFERENCES Course,
    FOREIGN KEY (StudentId) REFERENCES Student
);

-- ALTER TABLE and DROP TABLE also exist. 
-- special keywords 'IF EXISTS' useful for DROP/CREATE scripts

-- TRUNCATE TABLE 

INSERT INTO Course (CourseId,CourseName) 
VALUES (1,'Math'),
       (2,'Science'),
       (3,'Social Studies'),
       (4,'English')

INSERT INTO Student (StudentId, LastName, FirstName, MiddleName)
VALUES (1,'Jones', 'Bobby', 'Joe'),
       (2,'Smith', 'Joe', NULL),
       (3,'Winkle', 'Perry', NULL),
       (4,'Legend', 'I', 'Am'),
       (5,'Mary', 'Poppins', 'TheGOAT'),
       (6,'Bell', 'Tinker', NULL)

DECLARE @today DATETIME = GETDATE();

INSERT INTO Assignment (AssignmentId,AssignmentName,DueDate,SubmitDate,Grade,CourseId,StudentId)
VALUES (1,'math 1',           @today, DATEADD(DAY, +30, @today),  40, 1, 1),
       (2,'math 2',           @today, DATEADD(DAY, +11, @today),  50, 1, 2),
       (3,'science 1',        @today, DATEADD(DAY,  -1, @today),  60, 2, 3),
       (4,'science 2',        @today, DATEADD(DAY,  -1, @today),  80, 2, 4),
       (5,'social studies 1', @today, DATEADD(DAY, +15, @today),  65, 3, 5),
       (6,'social studies 2', @today, DATEADD(DAY,  +9, @today),  90, 3, 6),
       (7,'english 1',        @today, DATEADD(DAY,  +5, @today), 100, 4, 3),
       (8,'english 2',        @today, DATEADD(DAY, +50, @today),  85, 4, 6)
GO

-- DON'T know why he calls out bulk insert as a separate thing.
-- INSERT can be followed by SELECT statement - but select columns must match
-- insert statement 
DROP TABLE IF EXISTS CourseBackup;
CREATE TABLE CourseBackup (
    CourseId    INT        NOT NULL PRIMARY KEY,
    CourseName VARCHAR(50) NOT NULL,
    CourseDescripion NVARCHAR(MAX) NULL
)
INSERT INTO CourseBackup (CourseId,CourseName) 
SELECT CourseId,CourseName FROM Course -- cant use * here because of CourseDescription column
WHERE CourseId < 4 -- can filter  or do any other select magic, as long as columns match
SELECT * from CourseBackup
-- special syntax to create a table 
DROP TABLE IF EXISTS NewTable;
SELECT StudentId AS 'Student Id', FirstName + ' ' + LastName AS 'StudentName' 
INTO NewTable 
FROM Student

SELECT * FROM NewTable