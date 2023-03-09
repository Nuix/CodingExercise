CREATE PROCEDURE [dbo].[InsertUser]
	@userGuid uniqueidentifier
AS
	BEGIN TRY
		BEGIN TRANSACTION
			Insert into [dbo].[Users] values (@userGuid)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC [dbo].[GetErrorInfo]
	END CATCH
RETURN 0
