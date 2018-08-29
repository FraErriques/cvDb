USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_doc_multi_getId_at_refCandidatoId]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_doc_multi_getId_at_refCandidatoId]
	@ref_candidato_id int
as
select 
	dm.id 
	,sett.nomeSettore
	,dm.abstract
	,dm.sourceName
	,dm.insertion_time
from 
	doc_multi dm
	, candidato c
	, settoreCandidatura_LOOKUP sett
where 
	ref_candidato_id = @ref_candidato_id
	and abstract<>'_##__fake_abstract__##_'
	and c.id=dm.ref_candidato_id
	and sett.id = c.id_settore
GO
