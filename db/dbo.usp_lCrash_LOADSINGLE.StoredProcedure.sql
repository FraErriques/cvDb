USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_lCrash_LOADSINGLE]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_lCrash_LOADSINGLE]
	@id int
as
	select card from lCrash
	where id=@id
GO
