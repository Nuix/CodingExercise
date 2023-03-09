CREATE PROCEDURE [dbo].[DeleteInvestment]
	@investmentGuid uniqueidentifier
AS
	BEGIN TRY
	BEGIN TRANSACTION

	EXECUTE DeleteInvestmentRecord @InvestmentGuid = @investmentGuid
	DELETE FROM [dbo].[Investments] WHERE InvestmentGuid = @investmentGuid

	COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXECUTE [dbo].[GetErrorInfo]
	END CATCH

RETURN 0
