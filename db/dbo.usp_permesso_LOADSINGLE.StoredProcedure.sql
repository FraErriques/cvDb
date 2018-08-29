USE [cv_db]
GO
/****** Object:  StoredProcedure [dbo].[usp_permesso_LOADSINGLE]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_permesso_LOADSINGLE]
	@username varchar(50)
as
--
--
--
-- datatable utente
select ut.id id_utente, ut.username
from utente ut
	where ut.username = @username
--
-- datatable permesso
select  perm.id_permissionLevel, lev.permissionLevel, lev.nomeLivello
from  permesso perm, livelloFunzionario_LOOKUP lev
where lev.id=perm.id_permissionLevel
and perm.id_utente=(select id from utente where username=@username)
--
-- datatable settore
select  sett.id id_settore, sett.nomeSettore
from utente ut, settoreAziendale_LOOKUP sett, permesso perm
	where sett.id=isnull( perm.id_settore, 0)
	and perm.id_utente = ut.id
	and ut.username = @username
GO
