USE [ON_TIME_DB]
GO

/****** Object:  Table [dbo].[Frequency]    Script Date: 11/03/2019 11:56:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Frequency](
	[frequency_id] [int] NOT NULL,
	[frequency_description] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Frequency] PRIMARY KEY CLUSTERED 
(
	[frequency_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


