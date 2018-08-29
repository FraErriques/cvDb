USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_candidato_LOAD_search]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_candidato_LOAD_search]
as
select
	dm.id as doc
	,c.id as candidato
	, c.nominativo
	, dm.abstract
	, dm.insertion_time
from
	doc_multi	dm
	, candidato c
where 
	abstract <> '_##__fake_abstract__##_'
	and dm.ref_candidato_id = c.id
order by
	insertion_time desc
GO
