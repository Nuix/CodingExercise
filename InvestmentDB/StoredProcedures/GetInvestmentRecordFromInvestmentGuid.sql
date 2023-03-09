CREATE PROCEDURE [dbo].[GetInvestmentRecordFromInvestmentGuid]
	@InvestmentGuid uniqueidentifier
AS
	BEGIN TRY

		DECLARE @InvestmentId int
		SELECT @InvestmentId =  [InvestmentId] From [dbo].[Investments] WITH (NOLOCK)
		WHERE [dbo].[Investments].[InvestmentGuid] = @InvestmentGuid
	    
		IF (@InvestmentId = NULL)
			BEGIN
				Select NULL
			END
		ELSE
			BEGIN
				SELECT
				[DatePurchased]
				,[CostBasisPerShare]
				,[NumberOfShares]
				,[CurrentPrice]
				FROM [dbo].[InvestmentRecords] WITH (NOLOCK)
				WHERE [InvestmentRecords].InvestmentId = @InvestmentId
			END
	END TRY
	BEGIN CATCH
		EXECUTE [dbo].[GetErrorInfo]
	END CATCH
