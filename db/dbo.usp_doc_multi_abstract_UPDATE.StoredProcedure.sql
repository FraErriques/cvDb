USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_doc_multi_abstract_UPDATE]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_doc_multi_abstract_UPDATE]
@id int,
@_abstract varchar(5500)
as
	update
		[doc_multi]
	set
		[abstract]=@_abstract
	where id = @id
GO
