USE [master]
GO
/****** Object:  Database [Nuix]    Script Date: 2/21/2022 10:50:14 PM ******/
CREATE DATABASE [Nuix]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Nuix', FILENAME = N'C:\Nuix\Nuix.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Nuix_log', FILENAME = N'C:\Nuix\Nuix_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Nuix] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Nuix].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Nuix] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Nuix] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Nuix] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Nuix] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Nuix] SET ARITHABORT OFF 
GO
ALTER DATABASE [Nuix] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Nuix] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Nuix] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Nuix] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Nuix] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Nuix] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Nuix] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Nuix] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Nuix] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Nuix] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Nuix] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Nuix] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Nuix] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Nuix] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Nuix] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Nuix] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Nuix] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Nuix] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Nuix] SET  MULTI_USER 
GO
ALTER DATABASE [Nuix] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Nuix] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Nuix] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Nuix] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Nuix] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Nuix] SET QUERY_STORE = OFF
GO
USE [Nuix]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Nuix]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[DiscountPercent] [decimal](5, 2) NOT NULL,
	[EndDate] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[MinPurchase] [money] NOT NULL,
	[MaxPurchase] [money] NULL,
	[Percent] [decimal](5, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[Date] [date] NOT NULL,
	[Status] [smallint] NOT NULL,
	[TotalCost] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-20220220-181111]    Script Date: 2/21/2022 10:50:14 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20220220-181111] ON [dbo].[Order]
(
	[ID] ASC,
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[ProductCost] [money] NOT NULL,
	[Qty] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-20220220-181154]    Script Date: 2/21/2022 10:50:14 PM ******/
CREATE CLUSTERED INDEX [ClusteredIndex-20220220-181154] ON [dbo].[OrderDetail]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Cost] [money] NULL,
	[ItemNo] [int] NOT NULL,
	[EndDate] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-20220220-180832]    Script Date: 2/21/2022 10:50:14 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20220220-180832] ON [dbo].[Product]
(
	[ItemNo] ASC,
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddOrderItem]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Valetta, Steve
-- Create date: 2022-02-20
-- Description:	Add item to order or increment qty if ItemNo is already on the order
-- =============================================
CREATE PROCEDURE [dbo].[AddOrderItem] 
	-- Add the parameters for the stored procedure here
	@OrderID bigint,
	@ItemNo bigint,
	@Qty int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ProductID bigint;
	SET @ProductID = (SELECT ID FROM Product WHERE EndDate IS NULL AND ItemNo = @ItemNo);

    IF EXISTS (SELECT 1 FROM OrderDetail
			   WHERE ProductID = @ProductID
			     AND OrderID = @OrderID
			   )
	BEGIN
		UPDATE OrderDetail
		SET Qty = Qty + @Qty
		OUTPUT inserted.OrderID
		WHERE OrderID = @OrderID AND ProductID = @ProductID
	END
	ELSE
	BEGIN
		INSERT INTO OrderDetail(OrderID, ProductID, ProductCost, Qty)
		OUTPUT inserted.OrderID
		SELECT @OrderID, p.ID, p.Cost, @Qty
		FROM Product p
		WHERE EndDate IS NULL AND ItemNo = @ItemNo
	END
END
GO
/****** Object:  StoredProcedure [dbo].[CreateOrder]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Valetta, Steve
-- Create date: 2022-02-20
-- Description:	Create Order
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrder]
	@CustomerID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [Order](CustomerID, Date, Status) 
	OUTPUT Inserted.ID
	values (@CustomerID, GETDATE(), 10)
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteItem]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Valetta, Steve
-- Create date: 2022-02-21
-- Description:	Delete an item from an order
-- =============================================
CREATE PROCEDURE [dbo].[DeleteItem] 
	-- Add the parameters for the stored procedure here
	@OrderID bigint,
	@ProductID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM OrderDetail
	WHERE OrderID = @OrderID 
	  AND ProductID = @ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerSpend]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerSpend] 
	-- Add the parameters for the stored procedure here
	@StartDate date,
	@EndDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT o.CustomerID, sum(od.ProductCost)
	FROM OrderDetail od
	JOIN [Order] o
	  ON od.OrderID = o.ID
	JOIN Customer c
	  ON o.CustomerID = c.ID
	WHERE o.Date > @StartDate AND o.Date < @EndDate
	GROUP BY o.CustomerID
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderByOrderID]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Valetta, Steve
-- Create date: 2022-02-20
-- Description:	Get order and order details by Order ID
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderByOrderID] 
	@OrderID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT o.*, c.DiscountPercent
	FROM [Order] o
	JOIN Customer c ON c.ID = o.CustomerID
	WHERE o.ID = @OrderID;

	SELECT * 
	FROM [OrderDetail] 
	WHERE OrderID = @OrderID
	ORDER BY ID;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderItemQty]    Script Date: 2/21/2022 10:50:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateOrderItemQty]
	-- Add the parameters for the stored procedure here
	@OrderId bigint,
	@ItemNo int,
	@Qty int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE OrderDetail
	SET Qty = @Qty
	WHERE OrderID = @OrderId 
	  AND ProductID = (SELECT ID FROM Product WHERE EndDate IS NULL AND ItemNo = @ItemNo)
END
GO
USE [master]
GO
ALTER DATABASE [Nuix] SET  READ_WRITE 
GO
