CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserGuid] UNIQUEIDENTIFIER NOT NULL
)

GO

CREATE INDEX [IX_Users_UserId] ON [dbo].[Users] ([UserId], [UserGuid])

GO
