USE [master]
GO
/****** Object:  Database [cv_db]    Script Date: 08/29/2018 15:01:58 ******/
CREATE DATABASE [cv_db] ON  PRIMARY 
( NAME = N'cv_db', FILENAME = N'D:\root\dataSql\dat\cv_db.mdf' , SIZE = 1223872KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'cv_db_log', FILENAME = N'D:\root\dataSql\log\cv_db.ldf' , SIZE = 6912KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [cv_db] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cv_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cv_db] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [cv_db] SET ANSI_NULLS OFF
GO
ALTER DATABASE [cv_db] SET ANSI_PADDING OFF
GO
ALTER DATABASE [cv_db] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [cv_db] SET ARITHABORT OFF
GO
ALTER DATABASE [cv_db] SET AUTO_CLOSE ON
GO
ALTER DATABASE [cv_db] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [cv_db] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [cv_db] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [cv_db] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [cv_db] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [cv_db] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [cv_db] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [cv_db] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [cv_db] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [cv_db] SET  DISABLE_BROKER
GO
ALTER DATABASE [cv_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [cv_db] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [cv_db] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [cv_db] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [cv_db] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [cv_db] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [cv_db] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [cv_db] SET  READ_WRITE
GO
ALTER DATABASE [cv_db] SET RECOVERY SIMPLE
GO
ALTER DATABASE [cv_db] SET  MULTI_USER
GO
ALTER DATABASE [cv_db] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [cv_db] SET DB_CHAINING OFF
GO
GRANT CONNECT TO [applicationuser] AS [dbo]
GO