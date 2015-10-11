--Task1: Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN) and 
--Accounts(Id(PK), PersonId(FK), Balance).
--Insert few records for testing.
--Write a stored procedure that selects the full names of all persons.

USE [master]
GO

CREATE DATABASE Bank
GO

USE Bank
GO

CREATE TABLE Persons(
  ID int PRIMARY KEY IDENTITY(1, 1),
  FirstName nvarchar(100) NOT NULL,
  LastName nvarchar(100) NOT NULL,
  SSN nvarchar(50) NOT NULL
)
GO

CREATE TABLE Accounts(
  ID int PRIMARY KEY IDENTITY(1, 1),
  PersonID int NOT NULL,
  Balance money DEFAULT 0,
  CONSTRAINT FK_Persons_Accounts FOREIGN KEY (PersonID)
       REFERENCES Persons(ID)
)
GO

INSERT INTO Persons VALUES
('Ivan', 'Ivanov' , '123456789'),
('Peter', 'Petrov', '987654321'),
('Georgi', 'Georgiev', '112233445')

INSERT INTO Accounts VALUES
(1, 5000),
(2, 60000000),
(3, 20)
GO

CREATE PROCEDURE usp_SelectPeopleFullNames
  AS 
    SELECT FirstName + ' ' + LastName AS [Full name]
    FROM Persons
GO

EXEC usp_SelectPeopleFullNames
GO

--Task2: Create a stored procedure that accepts a number as a parameter and returns all persons who have more money in 
--their accounts than the supplied number.

CREATE PROCEDURE usp_PeopleWithMoreMoneyInTheirBankAccountThan(@amount money)
AS 
   SELECT p.FirstName + ' ' + p.LastName as [Full name], a.Balance
   FROM Persons p, Accounts a
   WHERE p.ID = a.PersonID AND a.Balance > @amount
GO

EXEC usp_PeopleWithMoreMoneyInTheirBankAccountThan 20
GO

--Task 3: Create a function that accepts as parameters – sum, yearly interest rate and number of months.
--It should calculate and return the new sum.
--Write a SELECT to test whether the function works as expected.

CREATE FUNCTION ufn_CalcuLateSumWithYearlyInterest(
  @sum money, @interestRate float, @months tinyInt) 
  RETURNS money 
AS
BEGIN
  RETURN @sum + @sum * (@interestRate/100/12) * @months
END
GO

SELECT Balance, dbo.ufn_CalcuLateSumWithYearlyInterest(Balance, 8, 3) AS [Sum with calculated interest]
FROM Accounts
GO

--Task 4: Create a stored procedure that uses the function from the previous example to give an interest to a person's 
--account for one month.
--It should take the AccountId and the interest rate as parameters.

CREATE PROCEDURE usp_GiveAnInterestForOneMonth(@id int, @rate float)
AS
   SELECT p.FirstName + ' ' + p.LastName AS [Full Name], a.Balance, 
          dbo.ufn_CalcuLateSumWithYearlyInterest(a.Balance, @rate, 1) AS [Sum with calculated interest for one month],
		  dbo.ufn_CalcuLateSumWithYearlyInterest(a.Balance, @rate, 1) - a.Balance AS [The interest for one month]
   FROM Persons p, Accounts a
   WHERE p.ID = @id AND @id = a.PersonID
GO

EXEC usp_GiveAnInterestForOneMonth 1, 12
GO

--Task 5: Add two more stored procedures WithdrawMoney(AccountId, money) and DepositMoney(AccountId, money) that operate 
--in transactions.

CREATE PROCEDURE usp_WithdrawMoney(@AccountId int, @money money)
AS
  BEGIN TRAN 
  UPDATE Accounts
  SET Balance -= @money
  WHERE ID = @AccountId
  COMMIT TRAN
GO

CREATE PROCEDURE usp_DepositMoney(@AccountId int, @money money)
AS
  BEGIN TRAN 
  UPDATE Accounts
  SET Balance += @money
  WHERE ID = @AccountId
  COMMIT TRAN
GO

EXEC usp_DepositMoney 3, 5000

EXEC usp_WithdrawMoney 3, 20
GO

--Task 6:  Create another table – Logs(LogID, AccountID, OldSum, NewSum).
--Add a trigger to the Accounts table that enters a new entry into the Logs table every time the sum on an account 
--changes.
CREATE TABLE Logs(
   LogID int PRIMARY KEY IDENTITY(1, 1),
   AccountID int NOT NULL FOREIGN KEY REFERENCES Accounts(ID),
   OldSum money NOT NULL,
   NewSum money NOT NULL
)
GO

CREATE TRIGGER tr_UpdateAccountBalance ON Accounts FOR UPDATE 
AS
   INSERT INTO Logs(AccountID, OldSum, NewSum)
   SELECT i.ID, d.Balance, i.Balance
   FROM deleted d, inserted i
GO

EXEC usp_WithdrawMoney 1, 3000
EXEC usp_DepositMoney 3, 200

--Task7: Define a function in the database TelerikAcademy that returns all Employee's names (first or middle or last name)
--and all town's names that are comprised of given set of letters.
--Example: 'oistmiahf' will return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.

USE TelerikAcademy
GO

CREATE FUNCTION ufn_SearchBySetOfLetters(@nameToCheck nvarchar(100), @letters nvarchar(100))
RETURNS INT
AS
BEGIN
   DECLARE @i int = 1
   DECLARE @currSymbol nvarchar(1) 
     WHILE( @i <= LEN(@nametoCheck)) 
	 BEGIN
	    SET @currSymbol = SUBSTRING(@nameToCheck, @i, 1)
		   IF (CHARINDEX(LOWER(@currSymbol), LOWER(@letters)) <= 0)
		      BEGIN
			   RETURN 0
			  END
		SET @i+=1
	 END
  RETURN 1
END
GO

SELECT e.FirstName + ' ' + e.LastName AS FullName, t.Name AS [Town Name]
FROM Employees e
JOIN Addresses a
  ON e.AddressID = a.AddressID
JOIN Towns t
  ON a.TownID = t.TownID
WHERE dbo.ufn_SearchBySetOfLetters(e.FirstName,'oistmiahf') = 1 OR 
dbo.ufn_SearchBySetOfLetters(e.LastName,'oistmiahf') = 1 OR
dbo.ufn_SearchBySetOfLetters(t.Name,'oistmiahf') = 1

--Task8:  Using database cursor write a T-SQL script that scans all employees and their addresses and prints all pairs 
--of employees that live in the same town.

DECLARE empCursor CURSOR READ_ONLY FOR
    SELECT emp1.FirstName, emp1.LastName, t1.Name, emp2.FirstName, emp2.LastName
    FROM Employees emp1
    JOIN Addresses a1
        ON emp1.AddressID = a1.AddressID
    JOIN Towns t1
        ON a1.TownID = t1.TownID,
        Employees emp2
    JOIN Addresses a2
        ON emp2.AddressID = a2.AddressID
    JOIN Towns t2
        ON a2.TownID = t2.TownID
    WHERE t1.TownID = t2.TownID AND emp1.EmployeeID != emp2.EmployeeID
    ORDER BY emp1.FirstName, emp2.FirstName

OPEN empCursor

DECLARE @firstName1 nvarchar(50), 
        @lastName1 nvarchar(50),
        @townName nvarchar(50),
        @firstName2 nvarchar(50),
        @lastName2 nvarchar(50)
FETCH NEXT FROM empCursor INTO @firstName1, @lastName1, @townName, @firstName2, @lastName2

DECLARE @counter int;
SET @counter = 0;

WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @counter = @counter + 1;

		PRINT @firstName1 + ' ' + @lastName1 + 
			  '     ' + @townName + '       ' +
			  @firstName2 + ' ' + @lastName2;

		FETCH NEXT FROM empCursor 
		INTO @firstName1, @lastName1, @townName, @firstName2, @lastName2
	END

print 'Total records: ' + CAST(@counter AS nvarchar(10));

CLOSE empCursor
DEALLOCATE empCursor

--Task9: Write a T-SQL script that shows for each town a list of all employees that live in it. 
CREATE TABLE #UsersTowns (ID INT IDENTITY, FullName NVARCHAR(50), TownName NVARCHAR(50))
INSERT INTO #UsersTowns
SELECT e.FirstName + ' ' + e.LastName, t.Name
                FROM Employees e
                        INNER JOIN Addresses a
                                ON a.AddressID = e.AddressID
                        INNER JOIN Towns t
                                ON t.TownID = a.TownID
                GROUP BY t.Name, e.FirstName, e.LastName
DECLARE @name NVARCHAR(50)
DECLARE @town NVARCHAR(50)
 
DECLARE employeeCursor CURSOR READ_ONLY FOR
        SELECT DISTINCT ut.TownName
                FROM #UsersTowns ut     
 
OPEN employeeCursor
FETCH NEXT FROM employeeCursor
	INTO @town
 
	WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @empName nvarchar(MAX);
			SET @empName = N'';
			SELECT @empName += ut.FullName + N', '
			FROM #UsersTowns ut
			WHERE ut.TownName = @town
			PRINT @town + ' -> ' + LEFT(@empName,LEN(@empName)-1);
			FETCH NEXT FROM employeeCursor INTO @town
		END
CLOSE employeeCursor
DEALLOCATE employeeCursor
DROP TABLE #UsersTowns

--Task10: Define a .NET aggregate function StrConcat that takes as input a sequence of strings and return a single 
--string that consists of the input strings separated by ','. 

USE TelerikAcademy
GO
IF NOT EXISTS (
    SELECT value
    FROM sys.configurations
    WHERE name = 'clr enabled' AND value = 1
)
BEGIN
    EXEC sp_configure @configname = clr_enabled, @configvalue = 1
    RECONFIGURE
END
GO
IF (OBJECT_ID('dbo.concat') IS NOT NULL) 
    DROP Aggregate concat; 
GO 
IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'concat_assembly') 
    DROP assembly concat_assembly; 
GO      
CREATE Assembly concat_assembly 
   AUTHORIZATION dbo 
   FROM 'D:\SqlStringConcatenation.dll' --- CHANGE THE LOCATION 
   WITH PERMISSION_SET = SAFE; 
GO 
CREATE AGGREGATE dbo.concat ( 
    @Value NVARCHAR(MAX),
    @Delimiter NVARCHAR(4000) 
) 
    RETURNS NVARCHAR(MAX) 
    EXTERNAL Name concat_assembly.concat; 
GO 
SELECT dbo.concat(FirstName + ' ' + LastName, ', ')
FROM Employees
GO
DROP Aggregate concat; 
DROP assembly concat_assembly; 
GO