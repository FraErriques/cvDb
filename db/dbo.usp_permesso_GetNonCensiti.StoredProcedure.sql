USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_permesso_GetNonCensiti]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_permesso_GetNonCensiti]
as	
--resultset utente
	select * from [dbo].[utente]
	where id not in( select id_utente from permesso)

--resultset permesso
select
	u.username,
	l.nomeLivello,
	s.nomeSettore
 from
	[permesso] p,
	[livelloFunzionario_LOOKUP] l,
	[utente] u,
	[settoreAziendale_LOOKUP] s
where 
	p.id_utente=u.id
	and l.id=p.id_permissionLevel
	and s.id= isnull(p.id_settore, 25)
order by nomeSettore
GO
