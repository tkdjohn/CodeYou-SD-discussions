/* 
multi line comments are just like C# /* and */ 
(note they can be embedded inside other multi line comments!)
*/

-- use double dash instead of // for single line comments
-- use ' instead of " for strings!

-- change to desired database
USE Courses;

SELECT * FROM Student

SELECT * FROM Assignment

-- CAN and SHOULD include database name with table name - dbo means 'database owner'
-- can be things other than dbo, but no one ever changes it
SELECT * FROM Courses.dbo.Course


-- avoid * for anything other than one off queries (especially production code)
-- separate multiple columns with a comma
SELECT AssignmentName, DueDate FROM Assignment

-- you can change the names of columns to be more human readable in the output 
-- this is called Column Aliasing
SELECT 
    AssignmentName AS Assignment
    , DueDate AS 'Due Date'
    , SubmitDate As 'Submitted On'
FROM Assignment

-- A query with a simple where clause that uses columns 
-- select all Assignments that were submitted before or on the due date
-- NOTE use of """  """ to allow us to format the SQL for readability

SELECT 
    AssignmentName AS Assignment, 
    DueDate AS 'Due On', 
    SubmitDate As 'Submitted On' 
FROM Assignment 
WHERE SubmitDate <= DueDate

-- Select all the Assignments with that were late and either aren't Course 
--   id 3 or have a D Grade (between 60 and 70)
SELECT 
        AssignmentName AS Assignment, 
        DueDate AS 'Due On', 
        SubmitDate As 'Submitted On', 
        Grade AS Grade,
        CourseId AS Course
    FROM Assignment 
    WHERE 
        SubmitDate > DueDate
        AND Grade BETWEEN 60 AND 70
        OR  CourseId != 3

/* OOPS! AND has precedence over OR so we need to add parentheses to make this work properly.
Note that between is inclusive. Both start and end are included. 
*/
-- #### Filtering with a list using IN and with wild-cards % and _

SELECT 
        AssignmentName AS Assignment, 
        DueDate AS 'Due On', 
        SubmitDate As 'Submitted On', 
        Grade AS Grade,
        CourseId AS Course
    FROM Assignment 
    WHERE 
        (Grade IN (80, 90, 100, 85) AND Grade NOT IN (20, 10, 40, 50 ,60) )
        OR AssignmentName LIKE '%c%'     -- for most SQL LIKE is case insensitive but that can be changed!
        OR AssignmentName NOT LIKE 'math _'

-- ##### Empty values (NULL)   
-- In SQL NULL is not Zero (and vice versa).
-- You can't use = and != with NULL. You must use IS NULL and IS NOT NULL.

/*
## Good coding practices in SQL

Avoid using Select *, specify column names instead. For several reasons:
* more readable
* performance costs with *
* fragility with *

Use parentheses for clarity.

Best practice capitalization is to put SQL keywords in ALL CAPS. Things like table names, column names 
and the like in all lower case. Obviously aliases should be capitalized or not as needed. 

Which is better 
*/
SELECT 
        AssignmentName AS Assignment, 
        DueDate AS 'Due On', 
        SubmitDate As 'Submitted On', 
        Grade AS Grade,
        CourseId AS Course
    FROM Assignment 
    WHERE 
        SubmitDate > DueDate
        AND  Grade BETWEEN 60 AND 70
        OR CourseId != 3

--or 

SELECT AssignmentName AS Assignment, DueDate AS 'Due On', SubmitDate As 'Submitted On', Grade AS Grade, CourseId AS Course FROM Assignment WHERE SubmitDate > DueDate AND  Grade BETWEEN 60 AND 70 OR CourseId != 3

 
/* 
 ### Additional Topics, time permitting

 * JOINS (using WHERE to join is .. ok.. using JOIN  ON is better)

 * ORDER BY <column_name>
 *    can specify ASCending or DESCending (ASC is typically the default if not specified 

 * SET FUNCTIONS (COUNT, MAX, MIN, SUM, AVG)
 *   COUNT(*) will include nulls count(column_name) won't 
 *   COUNT(1) (eg a constant) is a performance hack when you're looking for all rows, including nulls
 *   COUNT, MAX, MIN will work on all value types, AVG and SUM only numeric
 * QUALIFIERS with SET FUNCTIONS
 *   DISTINCT SELECT COUNT(DISTINCT FirstName) FROM Student
 *
 * GROUP BY - break the result into subsets - columns in the group by MUST be in SELECT LIST
 * HAVING - like where but for GROUP BY
 */


 /* more about joins
  * types of joins
  *    Cross Joins (don't - not super useful) has no WHERE and no JOIN
  *    INNER JOIN or just JOIN
  *        - joins a foreign key and a primary key (eg StudentId and CourseId are foreign keys 
  *          in the Assignments table which point to the primary keys in their respective keys)
  *        - only returns data when both tables have data that match the ON clause
  *    LET OUTER JOIN
  *        - requires keyword OUTER
  *        - return all the rows from the first table in the ON clause (with nulls for the columns
  *             in second table when no match)
  *        - can specify FULL OUTER JOIN to get all rows from BOTH tables (whit nulls for columns 
  *             that don't match between tables)
  *        - there is also RIGHT OUTER JOIN
  *        - defaults to LEFT if not specified
  *        
  * */

  /* we saw SELECT & INSERT
  there are also UPDATE and DELETE 
  */
  SELECT * FROM Student
  UPDATE Student 
  SET 
    FirstName = LastName
    ,LastName = 'Jones'
  WHERE FirstName = 'Bobby';
-- WHERE id =1 

  DELETE FROM Student 
  WHERE StudentId = 1