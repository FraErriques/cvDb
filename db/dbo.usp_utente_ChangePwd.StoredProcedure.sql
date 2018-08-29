USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_utente_ChangePwd]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[usp_utente_ChangePwd]
	@username varchar(50),
	@password varchar(50),
	@kkey     varchar(150),
	@mode     char(1)
as
UPDATE utente
SET
		password=@password,
		kkey=@kkey,
		mode=@mode
WHERE
	username=@username
GO
