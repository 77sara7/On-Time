USE [ON_TIME_DB]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 13/06/2019 12:56:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Request](
	[request_id] [int] IDENTITY(1,1) NOT NULL,
	[request_name] [nvarchar](30) NULL,
	[date_from] [date] NOT NULL,
	[date_to] [date] NOT NULL,
	[user_id] [int] NOT NULL,
	[day] [int] NULL,
	[hour] [int] NULL,
	[day_in_month] [int] NULL,
	[frequency_id] [int] NOT NULL,
	[file_id] [uniqueidentifier] NOT NULL,
	[recording_id] [uniqueidentifier] NULL,
	[is_relevant] [bit] NOT NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[request_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_is_relevant]  DEFAULT ((1)) FOR [is_relevant]
GO

ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_files] FOREIGN KEY([file_id])
REFERENCES [dbo].[files] ([stream_id])
GO

ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_files]
GO

ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_files1] FOREIGN KEY([recording_id])
REFERENCES [dbo].[files] ([stream_id])
GO

ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_files1]
GO

ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Frequency] FOREIGN KEY([frequency_id])
REFERENCES [dbo].[Frequency] ([frequency_id])
GO

ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Frequency]
GO

ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO

ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Users]
GO


