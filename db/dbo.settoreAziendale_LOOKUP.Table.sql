USE [cv_db]
GO
/****** Object:  Table [dbo].[settoreAziendale_LOOKUP]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[settoreAziendale_LOOKUP](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nomeSettore] [varchar](150) NOT NULL,
	[id_area] [int] NOT NULL,
 CONSTRAINT [pk_settoreAziendale] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[settoreAziendale_LOOKUP]  WITH CHECK ADD  CONSTRAINT [fk_settoreAziendale] FOREIGN KEY([id_area])
REFERENCES [dbo].[areaAziendale_LOOKUP] ([id])
GO
ALTER TABLE [dbo].[settoreAziendale_LOOKUP] CHECK CONSTRAINT [fk_settoreAziendale]
GO
