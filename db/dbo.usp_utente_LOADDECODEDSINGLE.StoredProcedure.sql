USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_utente_LOADDECODEDSINGLE]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[usp_utente_LOADDECODEDSINGLE]
	@id int
as
	select
		username
	from utente
	where id=@id
GO
