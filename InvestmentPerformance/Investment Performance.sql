USE [master]
GO
/****** Object:  Database [NuixInvestment]    Script Date: 2/21/2022 10:52:03 PM ******/
CREATE DATABASE [NuixInvestment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NuixInvestment', FILENAME = N'C:\Nuix\NuixInvestment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NuixInvestment_log', FILENAME = N'C:\Nuix\NuixInvestment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NuixInvestment] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NuixInvestment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NuixInvestment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NuixInvestment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NuixInvestment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NuixInvestment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NuixInvestment] SET ARITHABORT OFF 
GO
ALTER DATABASE [NuixInvestment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NuixInvestment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NuixInvestment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NuixInvestment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NuixInvestment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NuixInvestment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NuixInvestment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NuixInvestment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NuixInvestment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NuixInvestment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NuixInvestment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NuixInvestment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NuixInvestment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NuixInvestment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NuixInvestment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NuixInvestment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NuixInvestment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NuixInvestment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NuixInvestment] SET  MULTI_USER 
GO
ALTER DATABASE [NuixInvestment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NuixInvestment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NuixInvestment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NuixInvestment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NuixInvestment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NuixInvestment] SET QUERY_STORE = OFF
GO
USE [NuixInvestment]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [NuixInvestment]
GO
/****** Object:  Table [dbo].[Investments]    Script Date: 2/21/2022 10:52:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Investments](
	[InvestmentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CostBasisPerShare] [decimal](18, 2) NOT NULL,
	[CurrentValue] [decimal](18, 2) NOT NULL,
	[CurrentPrice] [decimal](18, 2) NOT NULL,
	[StockPurchaseDate] [date] NOT NULL,
	[TotalGainLoss] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Investments] PRIMARY KEY CLUSTERED 
(
	[InvestmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/21/2022 10:52:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [NuixInvestment] SET  READ_WRITE 
GO
