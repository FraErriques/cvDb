USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_doc_multi_get_sourceName]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_doc_multi_get_sourceName]
	@id int
as
select sourceName from doc_multi where id=@id
GO
