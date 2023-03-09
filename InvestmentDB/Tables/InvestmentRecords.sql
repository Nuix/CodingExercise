CREATE TABLE [dbo].[InvestmentRecords]
(
	[RecordId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvestmentId] INT NOT NULL, 
    [DatePurchased] DATETIME NOT NULL, 
    [CostBasisPerShare] DECIMAL(9, 2) NOT NULL, 
    [NumberOfShares] BIGINT NOT NULL, 
    [CurrentPrice] DECIMAL(9, 2) NOT NULL, 
    CONSTRAINT [FK_InvestmentRecords_Investments] FOREIGN KEY ([InvestmentId]) REFERENCES [Investments]([InvestmentId])
)

GO


CREATE INDEX [IX_InvestmentRecords_InvestmentId] ON [dbo].[InvestmentRecords] ([InvestmentId], [DatePurchased], [CostBasisPerShare], [NumberOfShares], [CurrentPrice])

GO

CREATE INDEX [IX_InvestmentRecords_RecordId_InvestmentId] ON [dbo].[InvestmentRecords] ([RecordId], [InvestmentId])
