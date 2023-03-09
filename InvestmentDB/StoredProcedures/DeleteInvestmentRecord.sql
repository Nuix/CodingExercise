CREATE PROCEDURE [dbo].[DeleteInvestmentRecord]
	@InvestmentGuid uniqueidentifier
AS
	DECLARE @InvestmentId int
	SELECT @InvestmentId = [Investments].[InvestmentId]
	FROM [dbo].[Investments] WITH (NOLOCK)
	WHERE InvestmentGuid = @InvestmentGuid

	IF (@InvestmentId = null)
		RETURN 1;

	BEGIN TRY
	Delete FROM [dbo].[InvestmentRecords] 
	WHERE InvestmentId = @InvestmentId
	END TRY
	BEGIN CATCH
		EXECUTE [dbo].[GetErrorInfo]
	END CATCH
RETURN 0
