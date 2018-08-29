USE [cv_db]
GO
/****** Object:  Table [dbo].[candidato]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[candidato](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nominativo] [varchar](150) NOT NULL,
	[id_settore] [int] NOT NULL,
	[note] [text] NULL,
 CONSTRAINT [pk_candidato] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[candidato]  WITH CHECK ADD  CONSTRAINT [fk_candidato_settore] FOREIGN KEY([id_settore])
REFERENCES [dbo].[settoreCandidatura_LOOKUP] ([id])
GO
ALTER TABLE [dbo].[candidato] CHECK CONSTRAINT [fk_candidato_settore]
GO
