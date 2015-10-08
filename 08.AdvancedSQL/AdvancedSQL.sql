USE TelerikAcademy

--Task1: Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company.
--Use a nested SELECT statement.
SELECT FirstName, LastName, Salary
FROM Employees 
WHERE Salary = 
    (SELECT MIN(Salary) FROM Employees)

--Task2: Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% 
--higher than the minimal salary for the company.
SELECT FirstName, LastName, Salary
FROM Employees 
WHERE Salary < 1.1 *
        (SELECT MIN(Salary) FROM Employees)
ORDER BY Salary

--Task3: Write a SQL query to find the full name, salary and department of the employees that take the minimal salary 
--in their department.
--Use a nested SELECT statement.
SELECT e.FirstName + ' ' + e.LastName AS [Full Name], e.Salary, d.Name AS [Department Name]
FROM Employees e
JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
WHERE e.Salary = (SELECT MIN(Salary) FROM Employees empl
                  WHERE empl.DepartmentID = d.DepartmentID)
ORDER BY Salary

--Task4: Write a SQL query to find the average salary in the department #1.
SELECT AVG(Salary) AS [Average Salary]
FROM Employees
WHERE DepartmentID = 1

--Task5: Write a SQL query to find the average salary in the "Sales" department.
SELECT AVG(Salary) AS [Average Salary]
FROM Employees
WHERE DepartmentID IN 
                  (SELECT DepartmentID FROM Departments
				   WHERE Name = 'Sales')

--Task6: Write a SQL query to find the number of employees in the "Sales" department.
SELECT COUNT(*) AS [Number of employees]
FROM Employees e
JOIN Departments d
  ON e.DepartmentID = d.DepartmentID AND d.Name = 'Sales'

--Task7: Write a SQL query to find the number of all employees that have manager.
SELECT COUNT(*) AS [Employees with a manager]
FROM Employees 
WHERE ManagerID IS NOT NULL

--Task8: Write a SQL query to find the number of all employees that have no manager.
SELECT COUNT(*) AS [Employees with no manager]
FROM Employees 
WHERE ManagerID IS NULL

--Task9: Write a SQL query to find all departments and the average salary for each of them.
SELECT AVG(Salary) AS [Average Salary], d.Name
FROM Employees e 
 JOIN Departments d
   ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name
ORDER BY [Average Salary]

--Task10: Write a SQL query to find the count of all employees in each department and for each town.
SELECT COUNT(*) AS [Number of employees], d.Name AS [Department], t.Name AS [Town]
FROM Employees e
 JOIN Departments d
   ON e.DepartmentID = d.DepartmentID
 JOIN Addresses a
   ON e.AddressID = a.AddressID
 JOIN Towns t
   ON t.TownID = a.TownID
GROUP BY d.Name, t.Name

--Task11:  Write a SQL query to find all managers that have exactly 5 employees. Display their first name and last name.
SELECT m.FirstName, m.LastName, COUNT(*) AS [Number of employees]
FROM Employees e
 JOIN Employees m
   ON e.ManagerID = m.EmployeeID
GROUP BY m.FirstName, m.LastName
HAVING COUNT(*) = 5
ORDER BY m.FirstName, m.LastName

--Task12: Write a SQL query to find all employees along with their managers. For employees that do not have manager 
--display the value "(no manager)".
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
       ISNULL(m.FirstName + ' ' + m.LastName, '(no manager)') AS [Manager Name]
FROM Employees e
LEFT OUTER JOIN Employees m
  ON e.ManagerID = m.EmployeeID

--Task13: Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. Use the 
--built-in LEN(str) function.
SELECT FirstName, LastName
FROM Employees 
WHERE LEN(LastName) = 5

--Task14: Write a SQL query to display the current date and time in the following format "day.month.year hour:minutes:seconds:milliseconds".
--Search in Google to find how to format dates in SQL Server.
SELECT CONVERT(VARCHAR(50),  GETDATE(), 104) + ' ' + CONVERT(VARCHAR(50), GETDATE(), 114) AS [Date and Time]
--SELECT FORMAT(GETDATE(), 'dd.MM.yyyy HH:mm:ss:fff')

--Task15:  Write a SQL statement to create a table Users. Users should have username, password, full name and last login time.
-- Choose appropriate data types for the table fields. Define a primary key column with a primary key constraint.
-- Define the primary key column as identity to facilitate inserting records.
-- Define unique constraint to avoid repeating usernames.
-- Define a check constraint to ensure the password is at least 5 characters long.
CREATE TABLE Users
   (
    UserID int PRIMARY KEY IDENTITY(1,1),
    UserName nvarchar(50) UNIQUE NOT NULL,
	Pass nvarchar(50) NOT NULL,
    FullName nvarchar(100) NOT NULL,
    LastLoginTime DateTime,
	CONSTRAINT [MinPasswordLength] CHECK (LEN(Pass) >= 5)
	)
GO

--Task16: Write a SQL statement to create a view that displays the users from the Users table that have been in the 
--system today.
--Test if the view works correctly.
CREATE VIEW [Users in the system today] AS
SELECT UserName
FROM Users
WHERE DAY(LastLoginTime) = DAY(GETDATE())
GO

--Task17: Write a SQL statement to create a table Groups. Groups should have unique name (use unique constraint).
--Define primary key and identity column.
CREATE TABLE Groups(
  GroupID int PRIMARY KEY IDENTITY(1, 1),
  Name nvarchar(50) UNIQUE NOT NULL
)
GO

--Task18: Write a SQL statement to add a column GroupID to the table Users.
-- Fill some data in this new column and as well in the `Groups table.
-- Write a SQL statement to add a foreign key constraint between tables Users and Groups tables.
ALTER TABLE Users
   ADD GroupID int NOT NULL, 
       CONSTRAINT FK_Users_Groups FOREIGN KEY (GroupID) REFERENCES Groups(GroupID)
GO

--Task19: Write SQL statements to insert several records in the Users and Groups tables.
INSERT INTO Groups VALUES
('Databases'),
('High quality code'),
('JS Applications'),
('JS UI')

INSERT INTO Users VALUES
( 'John', '12345', 'John Doe', GETDATE(), 1),
( 'Jane', '112233', 'Jane Doe', GETDATE(), 2),
( 'JImmy', '123456', 'Jimmy Jims', GETDATE(), 3)

--Task20: Write SQL statements to update some of the records in the Users and Groups tables
UPDATE Users SET UserName = 'Jonnyto', FullName = 'Johnnyto Doe'
WHERE UserName = 'John'

UPDATE Groups SET Name = 'No more JS Apps'
WHERE Name = 'JS Applications'

--Task21: Write SQL statements to delete some of the records from the Users and Groups tables.
DELETE FROM Groups
 WHERE Name = 'JS UI'

DELETE FROM Users
 WHERE UserName = 'Jane'

--Task22:  Write SQL statements to insert in the Users table the names of all employees from the Employees table.
--Combine the first and last names as a full name.
--For username use the first letter of the first name + the last name (in lowercase).
--Use the same for the password, and NULL for last login time.

--Slight modifications have been made to the requirements due to incompatibility with the constraints from the previous tasks
INSERT INTO Users
   SELECT LOWER(LEFT(e.FirstName, 3)) + LOWER(e.LastName) AS UserName,
          LOWER(LEFT(e.FirstName, 3)) + LOWER(e.LastName) AS Pass,
          CONCAT(FirstName, ' ', LastName) AS FullName,
		  NULL AS LastLoginTime,
		  1 AS GroupID
   FROM Employees e 

--Task23: Write a SQL statement that changes the password to NULL for all users that have not been in the system 
--since 10.03.2010.

-- Unfortunately this won't work due to the Pass_Length constraint. Change NULL to other value with length > 5
UPDATE Users SET Pass = 'NULL'
WHERE LastLoginTime < CONVERT(DATETIME, '2010-03-10')

--Task24: Write a SQL statement that deletes all users without passwords (NULL password).
DELETE FROM Users
WHERE Pass = 'NULL'

--Task25: Write a SQL query to display the average employee salary by department and job title.
SELECT AVG(e.Salary) AS [Average Salary], d.Name , e.JobTitle
FROM Employees e
 JOIN Departments d
 ON e.DepartmentID = d.DepartmentID
 GROUP BY d.Name, e.JobTitle
ORDER  BY d.Name,e.JobTitle

--Task26: Write a SQL query to display the minimal employee salary by department and job title along with the name of 
--some of the employees that take it.
SELECT MIN(e.Salary) AS [MinSalary], e.JobTitle, 
       e.FirstName + ' '+ e.LastName AS [Employee Name], d.Name AS [Department Name]
FROM Employees e
 JOIN Departments d
 ON e.DepartmentID = d.DepartmentID
 GROUP BY d.Name, e.JobTitle, e.FirstName + ' ' + e.LastName
ORDER  BY d.Name, e.JobTitle

--Task27:  Write a SQL query to display the town where maximal number of employees work.
SELECT TOP 1 t.Name, COUNT(e.EmployeeID) AS [Employees Total Count]
FROM Employees e
JOIN Addresses a
ON e.AddressID = a.AddressID
JOIN Towns t
ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY [Employees Total Count] DESC

--Task28: Write a SQL query to display the number of managers from each town.
SELECT COUNT(DISTINCT e.ManagerID) AS [Managers in town], t.Name AS [Town]
FROM Employees e
JOIN Employees em
ON e.ManagerID = em.EmployeeID
JOIN Addresses a
ON em.AddressID = a.AddressID
JOIN Towns t
ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY [Managers in town] DESC

--Task29: Write a SQL to create table WorkHours to store work reports for each employee (employee id, date, task, hours, comments).
--Don't forget to define identity, primary key and appropriate foreign key.
--Issue few SQL statements to insert, update and delete of some data in the table.
--Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
--For each change keep the old record data, the new record data and the command (insert / update / delete).

CREATE TABLE dbo.WorkHours(
  ID int PRIMARY KEY IDENTITY(1, 1),
  EmployeeID int NOT NULL,
  Date DATETIME NOT NULL,
  Task nvarchar(200) NOT NULL,
  Hours int NOT NULL,
  Comments nvarchar(200),
  CONSTRAINT FK_WorkHours_Employees FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID)
)

INSERT INTO WorkHours VALUES
(1, GETDATE(), 'Do something', 24, 'NO COMMENT'),
(2, GETDATE(), 'Do something more', 1, 'NO COMMENT'),
(3, '2004-05-23T14:25:10', 'Do something more more', 6, 'NO COMMENT')

UPDATE WorkHours
SET Task = 'Too late, dude'
WHERE Date = '2004-05-23T14:25:10'

DELETE FROM WorkHours
WHERE Hours < 5

CREATE TABLE WorkHoursLogs (
    WorkLogID int,
    EmployeeID Int NOT NULL,
    Date DATETIME NOT NULL,
    Task nvarchar(200) NOT NULL,
    Hours Int NOT NULL,
    Comments nvarchar(200),
    [Action] nvarchar(50) NOT NULL,
    CONSTRAINT FK_Employees_WorkHoursLogs
        FOREIGN KEY (EmployeeID)
        REFERENCES Employees(EmployeeID),
    CONSTRAINT [CC_WorkReportsLogs] CHECK ([Action] IN ('Insert', 'Delete', 'DeleteUpdate', 'InsertUpdate'))
) 
GO

CREATE TRIGGER tr_InsertWorkReports ON WorkHours FOR INSERT
AS
INSERT INTO WorkHoursLogs(WorkLogID, EmployeeID, Date, Task, [Hours], Comments, [Action])
    SELECT ID, EmployeeID, Date, Task, [Hours], Comments, 'Insert'
    FROM inserted
PRINT 'INSERT trigger fired.'
GO

CREATE TRIGGER tr_DeleteWorkReports ON WorkHours FOR DELETE
AS
INSERT INTO WorkHoursLogs(WorkLogID, EmployeeId, Date, Task, [Hours], Comments, [Action])
    SELECT ID, EmployeeID, Date, Task, [Hours], Comments, 'Delete'
    FROM deleted
PRINT 'DELETE trigger fired.'
GO

CREATE TRIGGER tr_UpdateWorkReports ON WorkHours FOR UPDATE
AS
INSERT INTO WorkHoursLogs(WorkLogID, EmployeeID, Date, Task, [Hours], Comments, [Action])
    SELECT ID, EmployeeID, Date, Task, [Hours], Comments, 'InsertUpdate'
    FROM inserted

INSERT INTO WorkHoursLogs(WorkLogID, EmployeeID, Date, Task, [Hours], Comments, [Action])
    SELECT ID, EmployeeID, Date, Task, [Hours], Comments, 'DeleteUpdate'
    FROM deleted
PRINT 'UPDATE trigger fired.'
GO

DELETE FROM WorkHours
WHERE Hours = 6

INSERT INTO WorkHours(EmployeeID, Date, Task, [Hours], Comments)
    VALUES (10, GETDATE(), 'One more task', 6, 'You can make it')

DELETE FROM WorkHours
WHERE EmployeeID = 20

UPDATE WorkHours
SET Comments = 'Updated'
 WHERE EmployeeID = 1

--Task30:  Start a database transaction, delete all employees from the 'Sales' department along with all dependent 7
--records from the pother tables.
--At the end rollback the transaction.
BEGIN TRAN
ALTER TABLE Departments
       DROP CONSTRAINT FK_Departments_Employees
GO

DELETE e
FROM Employees e
JOIN Departments d
  ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'

ROLLBACK TRAN

--Task31: Start a database transaction and drop the table EmployeesProjects. 
BEGIN TRAN 
  DROP TABLE EmployeesProjects
ROLLBACK TRAN

--Task32: Find how to use temporary tables in SQL Server.
--Using temporary tables backup all records from EmployeesProjects and restore them back after dropping and 
--re-creating the table.
BEGIN TRAN
SELECT *
INTO #TempEmployessProjects
FROM EmployeesProjects

DROP TABLE EmployeesProjects

SELECT *
INTO EmployeesProjects
FROM #TempEmployessProjects

DROP TABLE #TempEmployessProjects
ROLLBACK TRAN