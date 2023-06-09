USE [master]
GO
/****** Object:  Database [Zillow]    Script Date: 12/19/2022 6:37:47 PM ******/
CREATE DATABASE [Zillow]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Zillow', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Zillow.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Zillow_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Zillow_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Zillow] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Zillow].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Zillow] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Zillow] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Zillow] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Zillow] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Zillow] SET ARITHABORT OFF 
GO
ALTER DATABASE [Zillow] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Zillow] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Zillow] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Zillow] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Zillow] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Zillow] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Zillow] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Zillow] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Zillow] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Zillow] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Zillow] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Zillow] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Zillow] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Zillow] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Zillow] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Zillow] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Zillow] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Zillow] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Zillow] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Zillow] SET  MULTI_USER 
GO
ALTER DATABASE [Zillow] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Zillow] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Zillow] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Zillow] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Zillow]
GO
/****** Object:  Table [dbo].[PriceHistory]    Script Date: 12/19/2022 6:37:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NULL,
	[Date] [nvarchar](max) NULL,
	[Event] [nvarchar](max) NULL,
	[Price] [nvarchar](max) NULL,
 CONSTRAINT [PK_PriceHistory1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Properties]    Script Date: 12/19/2022 6:37:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](1000) NULL,
	[Address] [nvarchar](max) NULL,
	[ProviderName] [nvarchar](max) NULL,
	[ProviderPhone] [nvarchar](max) NULL,
	[BuiltIn] [nvarchar](255) NULL,
	[PriceSqft] [nvarchar](255) NULL,
	[Price] [nvarchar](255) NULL,
	[Bed] [nvarchar](255) NULL,
	[Bath] [nvarchar](255) NULL,
	[SqFt] [nvarchar](255) NULL,
	[Type] [nvarchar](max) NULL,
	[Zestimate] [nvarchar](255) NULL,
	[Latitude] [nvarchar](255) NULL,
	[Longitude] [nvarchar](255) NULL,
	[PropertyType] [nvarchar](max) NULL,
	[LotSize] [nvarchar](max) NULL,
	[LivableArea] [nvarchar](max) NULL,
	[ParkingSpaces] [nvarchar](255) NULL,
	[DaysOnZillow] [nvarchar](255) NULL,
	[ListUpdated] [nvarchar](max) NULL,
	[RentZestimate] [nvarchar](max) NULL,
 CONSTRAINT [PK_Properties1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyId]    Script Date: 12/19/2022 6:37:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyId](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyUrl] [nvarchar](max) NULL,
	[PropertyId] [nvarchar](max) NULL,
 CONSTRAINT [PK_PropertyId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaxHistory]    Script Date: 12/19/2022 6:37:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NULL,
	[Year] [nvarchar](max) NULL,
	[PropertyTax] [nvarchar](max) NULL,
	[TaxAssessment] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaxHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PriceHistory]  WITH CHECK ADD  CONSTRAINT [FK_PriceHistory_Properties] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Properties] ([Id])
GO
ALTER TABLE [dbo].[PriceHistory] CHECK CONSTRAINT [FK_PriceHistory_Properties]
GO
ALTER TABLE [dbo].[TaxHistory]  WITH CHECK ADD  CONSTRAINT [FK_TaxHistory_Properties] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Properties] ([Id])
GO
ALTER TABLE [dbo].[TaxHistory] CHECK CONSTRAINT [FK_TaxHistory_Properties]
GO
USE [master]
GO
ALTER DATABASE [Zillow] SET  READ_WRITE 
GO
