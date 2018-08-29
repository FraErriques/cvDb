USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_ViewCacher_specific_CREATE_candidato]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_ViewCacher_specific_CREATE_candidato]
	@where_tail varchar(1500)
	,@view_signature varchar(500)
as
declare @q varchar(7900)
	begin transaction
		begin try
			if @where_tail is NULL
			BEGIN
				select @where_tail = ''
			END -- else it's already a valid tail.
			--
			if @view_signature is NULL or Ltrim(Rtrim( @view_signature))=''
			begin
				-- RAISERROR with severity 11-19 will cause exeuction to jump to the CATCH block.
				RAISERROR( '---NB. @view_signature ----- must be specified and non-empty.'
						   ,16 -- Severity. -- RAISERROR with severity 11-19 will cause exeuction to jump to the CATCH block.
						   ,1 -- State.
						   );
			end--end if @view_signature is NULL else can continue.
-------------------------------------------------------------------------
			select @q =
			'
				create view ' 
				+ Ltrim(Rtrim( @view_signature))
				+' as
						SELECT
							ROW_NUMBER() OVER (ORDER BY c.nominativo asc) AS ''RowNumber''
							,c.id
							,c.nominativo
							,s.nomeSettore
							,c.note
				from
					candidato c
					,settoreCandidatura_LOOKUP s
				where c.id_settore=s.id '
				+ @where_tail
			exec( @q )
			-- if you get here, you can commit.
			commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	-- ready
GO
