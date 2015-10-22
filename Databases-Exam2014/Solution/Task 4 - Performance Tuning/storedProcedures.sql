USE Company
GO

CREATE PROC dbo.usp_CreateCacheTableForReports
AS
		CREATE TABLE ReportsCache(
		  EmployeeName nvarchar(41),
		  DepartmentName nvarchar(50),
		  ProjectName nvarchar(50),
		  StartDate date,
		  EndDate date,
		  TotalReports int
		)
GO

EXEC dbo.usp_CreateCacheTableForReports
GO

CREATE PROC dbo.usp_InsertCachedReports
AS
		DELETE FROM dbo.ReportsCache
		INSERT INTO dbo.ReportsCache 
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
GO

EXEC dbo.usp_InsertCachedReports
GO