USE [ON_TIME_DB]
GO

/****** Object:  StoredProcedure [dbo].[attached_file]    Script Date: 04/03/2019 12:20:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[attached_file](@file_name nvarchar(255),@file_data varbinary(max))
AS
DECLARE @file_id uniqueidentifier ;
BEGIN
SET  @file_id=NEWID();
		INSERT INTO files(stream_id,file_stream,name) VALUES(@file_id,@file_data,@file_name);
		SELECT stream_id,file_stream.PathNAme() as unc_path 
		FROM files
		WHERE stream_id=@file_id;
END
GO


