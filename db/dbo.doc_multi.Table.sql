USE [cv_db]
GO
/****** Object:  Table [dbo].[doc_multi]    Script Date: 08/29/2018 15:02:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[doc_multi](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ref_job_id] [int] NOT NULL,
	[ref_candidato_id] [int] NOT NULL,
	[abstract] [varchar](5500) NOT NULL,
	[sourceName] [varchar](350) NOT NULL,
	[doc] [image] NULL,
	[insertion_time] [datetime] NULL,
 CONSTRAINT [pk_doc_multi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[doc_multi]  WITH CHECK ADD  CONSTRAINT [fk_doc_multi_candidato] FOREIGN KEY([ref_candidato_id])
REFERENCES [dbo].[candidato] ([id])
GO
ALTER TABLE [dbo].[doc_multi] CHECK CONSTRAINT [fk_doc_multi_candidato]
GO
ALTER TABLE [dbo].[doc_multi] ADD  CONSTRAINT [DF__doc_multi__ref_j__34C8D9D1]  DEFAULT ((0)) FOR [ref_job_id]
GO
ALTER TABLE [dbo].[doc_multi] ADD  CONSTRAINT [DF__doc_multi__inser__35BCFE0A]  DEFAULT (getdate()) FOR [insertion_time]
GO
