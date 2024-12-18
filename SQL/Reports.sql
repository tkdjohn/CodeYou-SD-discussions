SELECT a.StudentId AS 'Student Id', s.FirstName,  s.LastName AS 'Student Name'
    FROM Student AS s
    INNER JOIN Assignment AS a
        ON a.StudentId = s.StudentId
    INNER JOIN Course AS c
        ON c.CourseId = a.CourseId
    WHERE 
        a.SubmitDate > a.DueDate


SELECT a.StudentId AS 'Student Id', S.FirstName + ' ' + S.LastName AS 'Student Name'
    FROM Student AS s
    INNER JOIN Assignment AS a
        ON a.StudentId = s.StudentId
    INNER JOIN Course AS c
        ON c.CourseId = a.CourseId
    WHERE 
        a.SubmitDate > a.DueDate


SELECT a.Grade, s.FirstName + ' ' + s.LastName AS 'Student Name'
    FROM Assignment a
    INNER JOIN Student s
        ON s.StudentId = a.StudentId
    INNER JOIN Course c
        ON c.CourseId = a.CourseId

SELECT AVG(a.Grade), s.FirstName + ' ' + s.LastName AS 'Student Name'
    FROM Assignment a
    INNER JOIN Student s
        ON s.StudentId = a.StudentId
    INNER JOIN Course c
        ON c.CourseId = a.CourseId
    GROUP BY
        a.StudentId, s.FirstName + ' ' + s.LastName