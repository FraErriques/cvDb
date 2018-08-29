USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_permesso_INSERT]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_permesso_INSERT]
	@id_utente int,
	@id_permissionLevel int,
	@id_settore int
as
insert into permesso(
	[id_utente],
	[id_permissionLevel],
	[id_settore]
   ) values(
		@id_utente,
		@id_permissionLevel,
		@id_settore 
	)
GO
