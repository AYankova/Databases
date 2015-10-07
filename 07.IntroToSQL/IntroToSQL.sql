USE TelerikAcademy

--Task 4: Write a SQL query to find all information about all departments (use "TelerikAcademy" database).
SELECT *
FROM Departments

--Task 5: Write a SQL query to find all department names.
SELECT Name AS [Department Name]
FROM Departments

--Task6: Write a SQL query to find the salary of each employee.
SELECT FirstName + ' ' + LastName AS [Full Name], Salary
FROM Employees
ORDER BY Salary

--Task7:  Write a SQL to find the full name of each employee.
SELECT FirstName + ' ' + LastName AS [Full Name]
From Employees

--Task8:  Write a SQL query to find the email addresses of each employee (by his first and last name). 
-- Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com". 
-- The produced column should be named "Full Email Addresses".
SELECT FirstName + '.' + LastName + '@telerik.com' AS [Full Email Addresses]
FROM Employees

--Task9: Write a SQL query to find all different employee salaries.
SELECT DISTINCT Salary
FROM Employees
ORDER BY Salary

--Task10: Write a SQL query to find all information about the employees whose job title is “Sales Representative“.
SELECT *
FROM Employees
WHERE JobTitle = 'Sales Representative'

--Task11: Write a SQL query to find the names of all employees whose first name starts with "SA".
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM Employees
WHERE FirstName LIKE 'SA%'

--Task12: Write a SQL query to find the names of all employees whose last name contains "ei".
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM Employees
WHERE LastName LIKE '%ei%'

--Task13: Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].
SELECT Salary
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000

--Task14: Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.
SELECT FirstName + ' ' + LastName AS [Full Name], Salary
FROM Employees
WHERE Salary IN (25000, 14000, 12500, 23600)
ORDER BY Salary

--Task 15: Write a SQL query to find all employees that do not have manager.
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM Employees
WHERE ManagerID IS NULL

--Task 16: Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary.
SELECT FirstName + ' ' + LastName AS [Full Name], Salary
FROM Employees
WHERE Salary > 50000
ORDER BY Salary DESC

--Task17: Write a SQL query to find the top 5 best paid employees.
SELECT TOP 5 FirstName, LastName, Salary
FROM Employees
ORDER BY Salary DESC

--Task18: Write a SQL query to find all employees along with their address. Use inner join with ON clause.
SELECT e.FirstName+ ' ' + e.LastName AS [Full Name], a.AddressText
FROM Employees e
INNER JOIN Addresses a
  ON e.AddressID=a.AddressID

--Task19: Write a SQL query to find all employees and their address. Use equijoins (conditions in the WHERE clause).
SELECT e.FirstName+ ' ' + e.LastName AS [Full Name], a.AddressText
FROM Employees e, Addresses a
WHERE e.AddressID = a.AddressID

--Task20: Write a SQL query to find all employees along with their manager.
SELECT e1.FirstName + ' ' + e1.LastName AS [Employee Name],
       e2.FirstName + ' ' + e2.LastName AS [Manager Name]
FROM Employees e1
LEFT OUTER JOIN Employees e2
   ON e1.ManagerID = e2.EmployeeID

--Task21: Write a SQL query to find all employees, along with their manager and their address. Join the 3 tables: 
--Employees e, Employees m and Addresses a.

SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
       m.FirstName + ' ' + m.LastName AS [Manager Name], 
	   a.AddressText AS 'Address'
FROM Employees e
 LEFT OUTER JOIN Employees m
   ON e.ManagerID = m.EmployeeID
 INNER JOIN Addresses a
   ON e.AddressID = a.AddressID

--Task22: Write a SQL query to find all departments and all town names as a single list. Use UNION.
SELECT Name
FROM Departments
UNION
 SELECT Name
 FROM Towns

--Task23: Write a SQL query to find all the employees and the manager for each of them along with the employees that 
--do not have manager. Use right outer join. Rewrite the query to use left outer join.
SELECT e1.FirstName + ' ' + e1.LastName AS [Employee Name],
       e2.FirstName + ' ' + e2.LastName AS [Manager Name]
FROM Employees e1
  LEFT OUTER JOIN Employees e2
    ON e1.ManagerID = e2.EmployeeID

SELECT e1.FirstName + ' ' + e1.LastName AS [Employee Name],
       e2.FirstName + ' ' + e2.LastName AS [Manager Name]
FROM Employees e2
  RIGHT OUTER JOIN Employees e1
     ON e1.ManagerID = e2.EmployeeID

--Task24: Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose 
--hire year is between 1995 and 2005.
SELECT e.FirstName + ' ' + e.LastName AS [Full Name], e.HireDate, 
       d.Name AS [Department Name]
FROM Employees e
  JOIN Departments d
      ON  (e.DepartmentID = d.DepartmentID
           AND d.Name IN ('Sales', 'Finance') 
	       AND YEAR(e.HireDate) BETWEEN 1995 AND 2005)
ORDER BY e.FirstName, e.LastName
