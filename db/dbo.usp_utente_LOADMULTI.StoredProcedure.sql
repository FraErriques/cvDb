USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_utente_LOADMULTI]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[usp_utente_LOADMULTI]
as
select
		[id],[username]
	from utente
GO
