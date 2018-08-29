USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_doc_multi_abstract_LOAD]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_doc_multi_abstract_LOAD]
@id int
as
select
	[abstract]
from
	[doc_multi]
where id = @id
GO
