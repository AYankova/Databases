USE Company
GO

--1
SELECT FirstName + ' ' + LastName AS [FullName], YearSalary
FROM Employees
WHERE YearSalary BETWEEN 100000 AND 150000
ORDER BY YearSalary

--2
SELECT d.Name, COUNT(e.Id) AS [CountEmployees]
FROM Employees e
INNER JOIN Departments d
ON e.DepartmentId = d.Id
GROUP BY d.Name
ORDER BY [CountEmployees] DESC

--3
SELECT e.FirstName + ' ' + e.LastName AS EmployeeName,
       d.Name AS DepartmentName, p.Name AS ProjectName,
	   pe.StartDate, pe.EndDate,
	   (SELECT COUNT(*) FROM Reports r
	   WHERE r.Time >= pe.StartDate AND r.Time<=pe.EndDate) AS TotalReports
FROM ProjectsEmployees pe
INNER JOIN Employees e
ON e.Id=pe.EmployeeId
INNER JOIN Departments d
ON d.Id=e.DepartmentId
INNER JOIN Projects p
ON p.Id=pe.ProjectId
ORDER BY pe.EmployeeId, pe.ProjectId

