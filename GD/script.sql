USE [master]
GO
/****** Object:  Database [Assignment8]    Script Date: 6/21/2018 1:14:23 PM ******/
CREATE DATABASE [Assignment8]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Assignment8', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER\MSSQL\DATA\Assignment8.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Assignment8_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER\MSSQL\DATA\Assignment8_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Assignment8] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Assignment8].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Assignment8] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Assignment8] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Assignment8] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Assignment8] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Assignment8] SET ARITHABORT OFF 
GO
ALTER DATABASE [Assignment8] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Assignment8] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Assignment8] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Assignment8] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Assignment8] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Assignment8] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Assignment8] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Assignment8] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Assignment8] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Assignment8] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Assignment8] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Assignment8] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Assignment8] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Assignment8] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Assignment8] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Assignment8] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Assignment8] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Assignment8] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Assignment8] SET  MULTI_USER 
GO
ALTER DATABASE [Assignment8] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Assignment8] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Assignment8] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Assignment8] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Assignment8] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Assignment8] SET QUERY_STORE = OFF
GO
USE [Assignment8]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Assignment8]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 6/21/2018 1:14:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ParentFolderId] [int] NOT NULL,
	[FileExt] [varchar](100) NOT NULL,
	[FileSizeInKB] [int] NOT NULL,
	[UploadedOn] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[uniqueName] [varchar](100) NOT NULL,
	[contentType] [varchar](100) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Folder]    Script Date: 6/21/2018 1:14:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[ParentFolderId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Folder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/21/2018 1:14:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[Login] [varchar](20) NULL,
	[Password] [varchar](20) NULL,
	[Email] [varchar](20) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Assignment8] SET  READ_WRITE 
GO
