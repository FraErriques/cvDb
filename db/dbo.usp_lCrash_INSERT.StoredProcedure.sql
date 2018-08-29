USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_lCrash_INSERT]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_lCrash_INSERT]
	@id int,
	@card int
as
	if @card is not null
	begin
		insert into [dbo].[lCrash](
			id,
			card
			) values(
				@id,
				@card
			)
	end
	else
	begin
		insert into [dbo].[lCrash](
			id
			) values(
				@id
				-- card defaults to zero
			)
	end
GO
