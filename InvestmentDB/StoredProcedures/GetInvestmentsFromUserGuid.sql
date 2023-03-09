CREATE PROCEDURE [dbo].[GetInvestmentsFromUserGuid]
	@UserGuid uniqueidentifier
AS
	BEGIN TRY

		DECLARE @UserId int
		Select @UserId = [UserId] FROM [dbo].[Users] WITH (NOLOCK)
		WHERE [users].[UserGuid] = @UserGuid

		IF (@UserId = null)
		BEGIN
			SELECT null
		END
		ELSE
		BEGIN
			SELECT [Investments].InvestmentGuid
			,[Investments].InvestmentName
			FROM [dbo].[Investments] WITH (NOLOCK)
			WHERE [Investments].[UserId] = @UserId
			ORDER BY [Investments].InvestmentId ASC
		END
	END TRY
	BEGIN CATCH
		EXEC [dbo].[GetErrorInfo]
	END CATCH
RETURN 0
