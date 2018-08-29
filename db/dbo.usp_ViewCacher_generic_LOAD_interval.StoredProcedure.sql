USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_ViewCacher_generic_LOAD_interval]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_ViewCacher_generic_LOAD_interval]
@min int
,@max int
,@view_signature varchar(500)
as
declare @cmd varchar(5500)--NB. no error check. On wrong name, throws.
--
select @cmd = ' select * from '
+ @view_signature
+ ' where RowNumber between  '
+ str( @min)
+ ' and '
+ str( @max)
--
exec( @cmd)
GO
