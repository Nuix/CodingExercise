USE [master]
GO

IF NOT EXISTS(
  SELECT
    1
  FROM
    sys.databases d
  WHERE
    d.name = N'Nuix')
  BEGIN
    CREATE DATABASE [Nuix]
  END
GO

USE [Nuix]
GO

IF NOT EXISTS(
  SELECT
    1
  FROM
    sys.tables t
  WHERE
    t.name = N'User')
  BEGIN
    CREATE TABLE [dbo].[User](
      [UserId] [bigint] IDENTITY(1,1) NOT NULL,
      [FirstName] [nvarchar](255) NULL,
      [LastName] [nvarchar](255) NULL,
      [Email] [nvarchar](255) NULL,
      CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
      (
        [UserId] ASC
      ))
  END
GO

IF NOT EXISTS(
  SELECT
    1
  FROM
    sys.tables t
  WHERE
    t.name = N'Investment')
  BEGIN
    CREATE TABLE [dbo].[Investment](
      [InvestmentId] [bigint] IDENTITY(1,1) NOT NULL,
      [UserId] [bigint] NOT NULL,
      [Name] [nvarchar](100) NULL,
      [Symbol] [varchar](5) NOT NULL,
      [PurchasedUTC] [datetime2](7) NOT NULL,
      [Quantity] [float] NOT NULL,
      [CostBasis] [money] NOT NULL,
      CONSTRAINT [PK_Investment] PRIMARY KEY CLUSTERED 
      (
        [InvestmentId] ASC
      ))
  END
GO

IF NOT EXISTS(
  SELECT
    1
  FROM
    sys.indexes i
  WHERE
    i.name = N'IX_Investment_UserId')
  BEGIN
    CREATE NONCLUSTERED INDEX [IX_Investment_UserId] ON [dbo].[Investment]
    (
      [UserId] ASC
    )
  END
GO

IF NOT EXISTS(
  SELECT
    1
  FROM
    sys.foreign_keys fk
  WHERE
    fk.name = N'FK_Investment_UserId_User_UserId')
  BEGIN
    ALTER TABLE [dbo].[Investment] WITH CHECK ADD CONSTRAINT [FK_Investment_UserId_User_UserId] FOREIGN KEY([UserId])
      REFERENCES [dbo].[User] ([UserId])
  END
GO

/* begin insert test data (if this were really a production release we wouldn't have this here */
IF NOT EXISTS(
  SELECT
    1
  FROM
    [User])
  BEGIN
    INSERT INTO [User] (FirstName, LastName, Email)
    VALUES
      ('Phil', 'Anselmo', 'panselmo@gmail.com'),
      ('Darrell', 'Abbott', 'dabbott@gmail.com'),
      ('Rex', 'Brown', 'rbrown@gmail.com'),
      ('Vinnie', 'Paul', 'vpaul@gmail.com')

    DECLARE @investments TABLE
    (
      [Symbol] [varchar](5) NOT NULL,
      [PurchasedUTC] [datetime2](7) NOT NULL,
      [Quantity] [float] NOT NULL,
      [CostBasis] [money] NOT NULL
    )

    INSERT INTO @investments(Symbol, PurchasedUTC, Quantity, CostBasis)
    VALUES
      ('UWMC', DATEADD(dd, (FLOOR(RAND()*(730-1+1))+1), GETUTCDATE()), (RAND()*(1000-1)+1), (RAND()*(100-1)+1)),
      ('GME', DATEADD(dd, (FLOOR(RAND()*(730-1+1))+1), GETUTCDATE()), (RAND()*(1000-1)+1), (RAND()*(100-1)+1)),
      ('GOOG', DATEADD(dd, (FLOOR(RAND()*(730-1+1))+1), GETUTCDATE()), (RAND()*(1000-1)+1), (RAND()*(100-1)+1)),
      ('AAPL', DATEADD(dd, (FLOOR(RAND()*(730-1+1))+1), GETUTCDATE()), (RAND()*(1000-1)+1), (RAND()*(100-1)+1))

    INSERT INTO Investment(UserId, [Name], Symbol, PurchasedUTC, Quantity, CostBasis)
    SELECT
      u.UserId,
      'Investment #' + CONVERT(VARCHAR(10), ROW_NUMBER() OVER (ORDER BY u.UserId)),
      i.Symbol,
      i.PurchasedUTC,
      i.Quantity,
      i.CostBasis
    FROM
      @investments i
      LEFT JOIN [User] u
      ON u.UserId = u.UserId
  END
GO
/* end insert test data */