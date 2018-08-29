USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_candidato_note_LOAD]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_candidato_note_LOAD]
@id int
as
select
	[note]
from
	candidato
where id = @id
GO
