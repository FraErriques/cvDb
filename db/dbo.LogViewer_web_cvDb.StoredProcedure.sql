USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[LogViewer_web_cvDb]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[LogViewer_web_cvDb]
	@startDate varchar(14),
	@endDate varchar(14)
as
select
	--[when] as full_timestamp,
	substring([when], 1,4) as [anno],
	case
		when substring([when], 5,2)=1 then 'Gennaio'
		when substring([when], 5,2)=2 then 'Febbraio'
		when substring([when], 5,2)=3 then 'Marzo'
		when substring([when], 5,2)=4 then 'Aprile'
		when substring([when], 5,2)=5 then 'Maggio'
		when substring([when], 5,2)=6 then 'Giugno'
		when substring([when], 5,2)=7 then 'Luglio'
		when substring([when], 5,2)=8 then 'Agosto'
		when substring([when], 5,2)=9 then 'Settembre'
		when substring([when], 5,2)=10 then 'Ottobre'
		when substring([when], 5,2)=11 then 'Novembre'
		when substring([when], 5,2)=12 then 'Dicembre'
		else 'Invalid Month'
	end   as month_name,
	--substring([when], 5,2) as [mese], if you want month-ordinal instead of month-name.
	substring([when], 7,2) as [giorno],
	substring([when], 9,2)+':'+substring([when], 11,2) as [ora_minuto],
	substring([when],13,2) as [secondo],
	function_name as procedure_called,
	content	as [message],
	-- campi tecnici per il debug su server
	row_nature,
	stack_depth
from
	[Logging].[dbo].[cv_db] --hard to let it a parameter; the whole query should become a string of dynamic sql.
where 
	convert(datetime,substring([when],1,8))>=convert(datetime,@startDate)
	and convert(datetime,substring([when],1,8))<=convert(datetime,@endDate)
	--and row_nature='t'
order by [when] desc
GO
