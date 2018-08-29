USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_candidato_LOAD]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_candidato_LOAD]
@where_tail varchar(5500)
as
declare @code varchar(5500)
if @where_tail is NULL
BEGIN
	select @where_tail = ''
END -- else it's already a valid tail.
select @code =
	'
	select
		c.id
		,c.nominativo
		,s.nomeSettore
		,c.note
	from 
		candidato c
		,settoreCandidatura_LOOKUP s
	where c.id_settore=s.id
	'
+ @where_tail
exec( @code)
GO
