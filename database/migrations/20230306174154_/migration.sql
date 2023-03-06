BEGIN TRY

BEGIN TRAN;

-- CreateTable
CREATE TABLE [dbo].[User] (
    [id] INT NOT NULL IDENTITY(1,1),
    [name] VARCHAR(255) NOT NULL,
    [email] VARCHAR(255) NOT NULL,
    [created_at] DATETIME2 NOT NULL CONSTRAINT [User_created_at_df] DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT [User_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[UserInvestments] (
    [user_id] INT NOT NULL,
    [investment_id] INT NOT NULL,
    [purchase_value] DECIMAL(32,16) NOT NULL,
    [shares] INT NOT NULL,
    [created_at] DATETIME2 NOT NULL,
    CONSTRAINT [UserInvestments_pkey] PRIMARY KEY CLUSTERED ([user_id],[investment_id])
);

-- CreateTable
CREATE TABLE [dbo].[Investment] (
    [id] INT NOT NULL IDENTITY(1,1),
    [name] NVARCHAR(1000) NOT NULL,
    [current_value] DECIMAL(32,16) NOT NULL,
    [created_at] DATETIME2 NOT NULL,
    CONSTRAINT [Investment_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateIndex
CREATE NONCLUSTERED INDEX [UserDecisions_user_id_fkey] ON [dbo].[UserInvestments]([user_id]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [UserDecisions_decision_id_fkey] ON [dbo].[UserInvestments]([investment_id]);

-- AddForeignKey
ALTER TABLE [dbo].[UserInvestments] ADD CONSTRAINT [UserInvestments_user_id_fkey] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User]([id]) ON DELETE CASCADE ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[UserInvestments] ADD CONSTRAINT [UserInvestments_investment_id_fkey] FOREIGN KEY ([investment_id]) REFERENCES [dbo].[Investment]([id]) ON DELETE CASCADE ON UPDATE NO ACTION;

COMMIT TRAN;

END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
BEGIN
    ROLLBACK TRAN;
END;
THROW

END CATCH
