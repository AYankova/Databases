USE [master]
GO
/****** Object:  Database [ClientsDB]    Script Date: 10/5/2015 22:19:56 ******/
CREATE DATABASE [ClientsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClientsDB', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ClientsDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ClientsDB_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ClientsDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ClientsDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClientsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClientsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClientsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClientsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClientsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClientsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClientsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClientsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClientsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClientsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClientsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClientsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClientsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClientsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClientsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClientsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ClientsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClientsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClientsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClientsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClientsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClientsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClientsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClientsDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ClientsDB] SET  MULTI_USER 
GO
ALTER DATABASE [ClientsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClientsDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClientsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClientsDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ClientsDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ClientsDB', N'ON'
GO
USE [ClientsDB]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 10/5/2015 22:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressText] [text] NOT NULL,
	[TownId] [int] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Continents]    Script Date: 10/5/2015 22:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Continents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Continents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 10/5/2015 22:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ContinentId] [int] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Persons]    Script Date: 10/5/2015 22:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Towns]    Script Date: 10/5/2015 22:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Towns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Towns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([Id], [AddressText], [TownId]) VALUES (4, N'Santa Monika', 3)
INSERT [dbo].[Addresses] ([Id], [AddressText], [TownId]) VALUES (6, N'Santa Lucia', 5)
INSERT [dbo].[Addresses] ([Id], [AddressText], [TownId]) VALUES (7, N'Santa Maria', 1)
SET IDENTITY_INSERT [dbo].[Addresses] OFF
SET IDENTITY_INSERT [dbo].[Continents] ON 

INSERT [dbo].[Continents] ([Id], [Name]) VALUES (1, N'Europe')
INSERT [dbo].[Continents] ([Id], [Name]) VALUES (2, N'Africa')
INSERT [dbo].[Continents] ([Id], [Name]) VALUES (3, N'Asia')
INSERT [dbo].[Continents] ([Id], [Name]) VALUES (4, N'Australia')
INSERT [dbo].[Continents] ([Id], [Name]) VALUES (5, N'South America')
INSERT [dbo].[Continents] ([Id], [Name]) VALUES (6, N'North America')
SET IDENTITY_INSERT [dbo].[Continents] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (1, N'Bulgaria', 1)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (2, N'Japan', 3)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (3, N'USA', 6)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (4, N'Argentina', 5)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (5, N'Australia', 4)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (6, N'Canada', 6)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (7, N'Morocco', 2)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (8, N'France', 1)
INSERT [dbo].[Countries] ([Id], [Name], [ContinentId]) VALUES (9, N'Norway', 1)
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [AddressId]) VALUES (1, N'Pancho', N'De Villa', 6)
INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [AddressId]) VALUES (2, N'Lucinda', N'Pereira', 4)
INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [AddressId]) VALUES (3, N'Miguel', N'Sanches', 7)
SET IDENTITY_INSERT [dbo].[Persons] OFF
SET IDENTITY_INSERT [dbo].[Towns] ON 

INSERT [dbo].[Towns] ([Id], [Name], [CountryId]) VALUES (1, N'Sofia', 1)
INSERT [dbo].[Towns] ([Id], [Name], [CountryId]) VALUES (2, N'Washington DC', 3)
INSERT [dbo].[Towns] ([Id], [Name], [CountryId]) VALUES (3, N'Las Vegas', 3)
INSERT [dbo].[Towns] ([Id], [Name], [CountryId]) VALUES (4, N'Paris', 8)
INSERT [dbo].[Towns] ([Id], [Name], [CountryId]) VALUES (5, N'Sydney', 5)
SET IDENTITY_INSERT [dbo].[Towns] OFF
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Towns] FOREIGN KEY([TownId])
REFERENCES [dbo].[Towns] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Towns]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Continents] FOREIGN KEY([ContinentId])
REFERENCES [dbo].[Continents] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Continents]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Addresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Addresses]
GO
ALTER TABLE [dbo].[Towns]  WITH CHECK ADD  CONSTRAINT [FK_Towns_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Towns] CHECK CONSTRAINT [FK_Towns_Countries]
GO
USE [master]
GO
ALTER DATABASE [ClientsDB] SET  READ_WRITE 
GO
