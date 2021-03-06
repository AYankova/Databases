USE [ToysStore]
GO

--1
SELECT Name, Price
FROM Toys
WHERE Type = 'puzzle' AND Price > 10
ORDER BY Price 

--2
SELECT m.Name,
(SELECT COUNT (*)
FROM Toys t
INNER JOIN AgeRanges a
ON t.AgeRangeId = a.Id 
WHERE a.MinimumAge >= 5 AND a.MaximumAge <= 10 AND m.Id = t.ManufacturerId) AS [Count]
FROM Manufacturers m

--3
SELECT t.Name, t.Price, t.Color
FROM Toys t
INNER JOIN ToysCategories tc
ON t.Id = tc.ToyId
INNER JOIN Categories c
ON c.Id = tc.CategoryId
WHERE c.Name = 'boys'