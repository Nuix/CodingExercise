CREATE PROCEDURE [dbo].[InsertInvestment]
	@userGuid uniqueidentifier,
	@investmentGuid uniqueidentifier,
	@investmentName nvarchar(10)
AS
BEGIN TRY
	DECLARE @userId int
	SELECT @userId = [dbo].[Users].UserId 
	FROM [dbo].[Users] WITH (NOLOCK)
	WHERE [dbo].[Users].[UserGuid] = @userGuid

	if (@userId = null)
		RETURN 1
	ELSE
	BEGIN
		BEGIN TRANSACTION
		INSERT INTO [dbo].[Investments] 
		values (@investmentGuid, @userId, @investmentName)
		COMMIT TRANSACTION
	END
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	EXEC GetErrorInfo
END CATCH
			
RETURN 0
