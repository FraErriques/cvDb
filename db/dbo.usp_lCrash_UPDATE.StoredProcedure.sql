USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_lCrash_UPDATE]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_lCrash_UPDATE]
	@id int,
	@card int
as
	update  [dbo].[lCrash]
		set card=@card
	where id=@id
GO
