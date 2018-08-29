USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_candidato_note_UPDATE]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_candidato_note_UPDATE]
@id int,
@note text
as
	update
		[candidato]
	set
		[note]=@note
	where id = @id
GO
