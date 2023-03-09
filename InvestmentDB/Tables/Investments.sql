CREATE TABLE [dbo].[Investments]
(
	[InvestmentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvestmentGuid] UNIQUEIDENTIFIER NOT NULL, 
    [UserId] INT NOT NULL, 
    [InvestmentName] NCHAR(10) NOT NULL, 
    CONSTRAINT [FK_Investments_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
)

GO

CREATE INDEX [IX_Investments_InvestmentId_InvestmentGuid] ON [dbo].[Investments] ([InvestmentId], [InvestmentGuid])
GO
CREATE INDEX [IX_Investments_UserId_InvestmentGuid_InvestmentName] ON [dbo].[Investments] ([UserId], [InvestmentGuid], [InvestmentName])
GO
