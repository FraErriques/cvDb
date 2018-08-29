USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_candidato_LOAD_Description]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_candidato_LOAD_Description]
	@id int
as
select 
	c.nominativo
	,s.nomeSettore
from 
	candidato c
	,settoreCandidatura_LOOKUP s
where
	c.id = @id
	and s.id = c.id_settore
GO
