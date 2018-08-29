USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_candidato_INSERT]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_candidato_INSERT]
	-- id
	@nominativo varchar(150)
	,@id_settore int
	,@note text
as
insert into candidato(
	-- id
	nominativo
	,id_settore
	,note
)values(
	-- id
	@nominativo
	,@id_settore
	,@note
)
GO
