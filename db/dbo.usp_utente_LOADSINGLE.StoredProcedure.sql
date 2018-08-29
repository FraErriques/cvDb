USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_utente_LOADSINGLE]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[usp_utente_LOADSINGLE]
	@username varchar(50)
as
	select
		*
	from utente
	where username=@username
GO
