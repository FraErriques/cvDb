USE [cv_db]
GO
/****** Object:  Table [dbo].[utente]    Script Date: 08/29/2018 15:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[utente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[kkey] [varchar](150) NULL,
	[mode] [char](1) NOT NULL,
 CONSTRAINT [pk_utente_idonly] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__utente__03317E3D] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[utente] ADD  CONSTRAINT [DF__utente__mode__0425A276]  DEFAULT ('m') FOR [mode]
GO