USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_utente_INSERT]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[usp_utente_INSERT]
	@username varchar(50),
	@password varchar(50),
	@kkey     varchar(150),
	@mode     char(1)
as
insert into utente(
--id
[username],
[password],
[kkey],
[mode]
       ) values(
--id
@username,
@password,
@kkey,
@mode
)
GO
