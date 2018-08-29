USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_settoreCandidatura_LOOKUP_INSERT]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_settoreCandidatura_LOOKUP_INSERT]
@nomeSettore varchar(150)
as
insert into [settoreCandidatura_LOOKUP](
-- id
nomeSettore
	) values(
@nomeSettore
)
GO
