USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_utente_UPDATE]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[usp_utente_UPDATE]
	@username varchar(50),
	@password varchar(50),
	@kkey varchar(150),
	@mode char(1)
as
	update utente
	set
		[password]=@password,
		[kkey]=@kkey,
		[mode]=@mode
	where username=@username
GO
