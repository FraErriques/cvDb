USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_doc_multi_getBlobAtId]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_doc_multi_getBlobAtId]
	@id_required_phase int
as
	-- returns a datatable of all columns, at id=@id_required_phase.
	select * from doc_multi where id=@id_required_phase
	--ready
GO
