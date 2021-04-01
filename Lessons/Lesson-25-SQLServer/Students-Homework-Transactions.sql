UPDATE Students
SET LastName = 'WELL DONE'
WHERE EXISTS
(SELECT h.UserId
	FROM HomeWorks as h
	WHERE Students.Id = h.UserId and h.Mark = 3)

	SELECT [Name], [LastName]
	FROM Students
	ORDER BY [NAME] DESC


	DECLARE @pageSize as int = 3;

	SELECT * FROM 
		(SELECT [Name], [UserId],
			ROW_NUMBER() OVER(PARTITION BY UserId ORDER BY [Name] DESC) as RowNum
		FROM HomeWorks) AS Paged
	WHERE RowNum > @pageSize and RowNum <= 2*@pageSize

	
BEGIN TRANSACTION
		select * from HomeWorks
		order by UserId
		-- Valery Silakov

		-- update

		select * from HomeWorks
		order by [Name]
		-- Valery Nesilakov
COMMIT

BEGIN TRANSACTION

-- DDL
-- DML

-- ROLLBACK
COMMIT

COMMIT

UPDATE Students
SET LastName = 'WELL DONE'
WHERE Id = 3