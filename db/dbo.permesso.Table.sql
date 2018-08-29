USE [cv_db]
GO
/****** Object:  Table [dbo].[permesso]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permesso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_utente] [int] NOT NULL,
	[id_permissionLevel] [int] NOT NULL,
	[id_settore] [int] NULL,
 CONSTRAINT [pk_permesso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[permesso]  WITH CHECK ADD  CONSTRAINT [fk_permessoPermissionLevel] FOREIGN KEY([id_permissionLevel])
REFERENCES [dbo].[livelloFunzionario_LOOKUP] ([id])
GO
ALTER TABLE [dbo].[permesso] CHECK CONSTRAINT [fk_permessoPermissionLevel]
GO
ALTER TABLE [dbo].[permesso]  WITH CHECK ADD  CONSTRAINT [fk_permessoSettore] FOREIGN KEY([id_settore])
REFERENCES [dbo].[settoreAziendale_LOOKUP] ([id])
GO
ALTER TABLE [dbo].[permesso] CHECK CONSTRAINT [fk_permessoSettore]
GO
ALTER TABLE [dbo].[permesso]  WITH CHECK ADD  CONSTRAINT [fk_permessoUtente] FOREIGN KEY([id_utente])
REFERENCES [dbo].[utente] ([id])
GO
ALTER TABLE [dbo].[permesso] CHECK CONSTRAINT [fk_permessoUtente]
GO
