CREATE PROCEDURE [dbo].[DeleteUser]
	@userGuid uniqueidentifier
AS
	BEGIN TRY
	DELETE FROM [dbo].[Users] WHERE [Users].UserGuid = @userGuid
	END TRY

	BEGIN CATCH
		EXECUTE [dbo].[GetErrorInfo]
	END CATCH
RETURN 0
