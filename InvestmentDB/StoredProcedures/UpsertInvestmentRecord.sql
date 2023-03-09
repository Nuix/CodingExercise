CREATE PROCEDURE [dbo].[UpsertInvestmentRecord]
	@investmentGuid uniqueidentifier,
	@datePurchased DateTime,
	@costBasisPerShare decimal(9,2),
	@numberOfShares bigint,
	@currentPrice decimal(9,2)
AS
	DECLARE @investmentId int
	SELECT @investmentId = [dbo].[Investments].InvestmentId
	FROM [dbo].[Investments] WITH (NOLOCK)
	WHERE [dbo].[Investments].[InvestmentGuid] = @investmentGuid

	If (@investmentGuid = null)
		RETURN 1

	BEGIN TRY

	BEGIN TRANSACTION
	IF (EXISTS(Select [InvestmentId] from [dbo].[InvestmentRecords] WITH (NOLOCK) WHERE [InvestmentId] = @investmentId))
		UPDATE [dbo].[InvestmentRecords] 
		SET  [DatePurchased] = @datePurchased,
			[CostBasisPerShare] = @costBasisPerShare,
			[NumberOfShares] = @numberOfShares,
			[CurrentPrice] = @currentPrice
		WHERE [InvestmentId] = @investmentId
	ELSE
	BEGIN
		INSERT INTO [dbo].[InvestmentRecords]
		Values(@investmentId, @datePurchased, @costBasisPerShare, @numberOfShares, @currentPrice)
	END
	COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC [dbo].[GetErrorInfo]
	END CATCH
RETURN 0
