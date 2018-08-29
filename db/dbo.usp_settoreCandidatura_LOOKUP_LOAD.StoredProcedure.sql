USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_settoreCandidatura_LOOKUP_LOAD]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[usp_settoreCandidatura_LOOKUP_LOAD]
as
select * from [dbo].[settoreCandidatura_LOOKUP]
order by nomeSettore asc
GO
