CREATE TABLE [dbo].[HomeWorks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserId] INT NOT NULL, 
    [Name] NVARCHAR(150) NOT NULL, 
    [PullRequestUrl] NVARCHAR(250) NULL, 
    [Mark] CHAR(10) NULL, 
    [IsComplete] BIT NOT NULL, 
    CONSTRAINT [FK_HomeWorks_ToTable] FOREIGN KEY ([UserId]) REFERENCES [Students]([Id])
)
