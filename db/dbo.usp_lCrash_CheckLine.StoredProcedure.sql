USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_lCrash_CheckLine]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_lCrash_CheckLine]
	@id int
as
	declare @res int
	select @res = (select count(id) from [dbo].[lCrash]	where id=@id)
	return @res
GO
