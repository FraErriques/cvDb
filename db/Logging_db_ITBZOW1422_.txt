USE [master]
GO
/****** Object:  Database [Logging]    Script Date: 08/30/2018 18:51:37 ******/
CREATE DATABASE [Logging] ON  PRIMARY 
( NAME = N'Logging', FILENAME = N'C:\root\dataSql\ExpressLie\dat\Logging.mdf' , SIZE = 5504KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Logging_log', FILENAME = N'C:\root\dataSql\ExpressLie\log\Logging.ldf' , SIZE = 3456KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
---------------------------------

ALTER DATABASE [Logging] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Logging].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [Logging] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Logging] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Logging] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Logging] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Logging] SET ARITHABORT OFF
GO
ALTER DATABASE [Logging] SET AUTO_CLOSE ON
GO
ALTER DATABASE [Logging] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Logging] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Logging] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Logging] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Logging] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Logging] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Logging] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Logging] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Logging] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Logging] SET  DISABLE_BROKER
GO
ALTER DATABASE [Logging] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Logging] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Logging] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Logging] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Logging] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Logging] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Logging] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Logging] SET  READ_WRITE
GO
ALTER DATABASE [Logging] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Logging] SET  MULTI_USER
GO
ALTER DATABASE [Logging] SET PAGE_VERIFY TORN_PAGE_DETECTION
GO
ALTER DATABASE [Logging] SET DB_CHAINING OFF
GO
USE [Logging]
GO



/****** Object:  User [applicationuser]    Script Date: 08/30/2018 18:51:37 ******/
CREATE USER [applicationuser] FOR LOGIN [applicationuser] WITH DEFAULT_SCHEMA=[dbo]
GO

/****** Object:  StoredProcedure [dbo].[trace]    Script Date: 08/30/2018 18:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE         procedure [dbo].[trace]
            @logname char(50),
            @when varchar(50),
            @row_nature char(3),
            @stack_depth varchar(5),
            @function_name varchar(50),
            @content varchar(7971)
      as
        declare @cmd char(8000)
        IF OBJECT_ID('Logging..'+@logname) IS NOT NULL
        BEGIN
    SET @cmd = 'insert into Logging..'+
    @logname+'(
    [when],
    [row_nature],
    [stack_depth],
    [function_name],
    [content] 	  ) values('+
        @when+', '+
        @row_nature+', '+
        @stack_depth+', '+
        @function_name+', '+
        @content+  ' )'
 -- print @cmd  debug only
 exec (@cmd)
 end
     /*
         else
         begin
         -- required table not found -> do nothing
         END
     */
GO
------------------------------------------------------------------------


/****** Object:  Table [dbo].[TracciatoConData]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TracciatoConData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[row_nature] [char](3) NOT NULL,
	[stack_depth] [varchar](5) NOT NULL,
	[function_name] [varchar](50) NOT NULL,
	[content] [varchar](7919) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
--------------------------------------


/****** Object:  Table [dbo].[PrimeDataRiemann]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrimeDataRiemann](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[row_nature] [char](3) NOT NULL,
	[stack_depth] [varchar](5) NOT NULL,
	[function_name] [varchar](50) NOT NULL,
	[content] [varchar](7919) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
----------------------------------------------------


/****** Object:  Table [dbo].[Francesco__127_0_0_1]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Francesco__127_0_0_1](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[row_nature] [char](3) NOT NULL,
	[stack_depth] [varchar](5) NOT NULL,
	[function_name] [varchar](50) NOT NULL,
	[content] [varchar](7919) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
-------------------------------------------------------



/****** Object:  Table [dbo].[cv_db_log]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cv_db_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[row_nature] [char](3) NOT NULL,
	[stack_depth] [varchar](5) NOT NULL,
	[function_name] [varchar](50) NOT NULL,
	[content] [varchar](7919) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
---------------------------------------------------------



/****** Object:  Table [dbo].[cv_db]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cv_db](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[row_nature] [char](3) NOT NULL,
	[stack_depth] [varchar](5) NOT NULL,
	[function_name] [varchar](50) NOT NULL,
	[content] [varchar](7919) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO



/****** Object:  StoredProcedure [dbo].[createLogTable]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE         procedure [dbo].[createLogTable]
    @logname char(50)
as

IF OBJECT_ID(@logname) IS NULL
BEGIN

declare @cmd varchar( 8000)

    SET @cmd = '
            CREATE TABLE Logging..'+@logname+' (

    [id] [int] IDENTITY (1, 1) NOT NULL ,
    [when] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [row_nature] char(3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [stack_depth] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [function_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [content] varchar(7919) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    PRIMARY KEY  CLUSTERED
    (
        [id]
    )  ON [PRIMARY] 
    ) ON [PRIMARY]'
-- cmd text ready
    exec (@cmd)
END
-- else table already exists
GO
---------------------------------------------------------------------




/****** Object:  Table [dbo].[Cespiti_log]    Script Date: 08/30/2018 18:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cespiti_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[when] [varchar](50) NOT NULL,
	[row_nature] [char](3) NOT NULL,
	[stack_depth] [varchar](5) NOT NULL,
	[function_name] [varchar](50) NOT NULL,
	[content] [varchar](7919) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
----------------------------------------------------------------------
