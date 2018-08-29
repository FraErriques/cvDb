USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_doc_multi_SEARCH_CandidateDocuments]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_doc_multi_SEARCH_CandidateDocuments]
	@id_set varchar( 6000)
as
-- NB. example of how to build this parameter from the application layer:
--declare @id_set varchar( 6000)
--select @id_set = ' (1,2,3)'

declare @query_mainPart varchar( 400)
select @query_mainPart =
'select 
		dm.id,
		c.nominativo as candidato,
		dm.abstract
	from
		[dbo].[doc_multi] dm,
		[dbo].[candidato]  c '
	+' where 
		abstract <> ''_##__fake_abstract__##_''
		and dm.ref_candidato_id = c.id '
	--print @query_mainPart
-- decide whether to add an ending part
declare @wholeQuery varchar( 7000)
if( @id_set is not null )
	begin
		select @wholeQuery = @query_mainPart 
		+ ' and dm.id in ' + @id_set 
		+ ' order by abstract'
		--print @wholeQuery
	end
else
	begin
		select @wholeQuery = @query_mainPart + ' order by abstract'
		--print @wholeQuery
	end
-- ready
exec (@wholeQuery)
GO
