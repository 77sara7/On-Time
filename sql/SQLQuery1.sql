USE [ON_TIME_DB]
GO
/****** Object:  StoredProcedure [dbo].[attached_file_add]    Script Date: 29/04/2019 12:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[attached_file_add](@file_name nvarchar(255),@file_data varbinary(max),@file_id uniqueidentifier OUT)
AS
--DECLARE @file_id uniqueidentifier ;
BEGIN

SET  @file_id=NEWID();
		INSERT INTO files(stream_id,file_stream,name) VALUES(@file_id,@file_data,@file_name);
		SELECT @file_id

		RETURN  

END


--ALTER PROCEDURE [dbo].[attached_file_add](@file_name nvarchar(255),@file_data varbinary(max),@file_id uniqueidentifier OUTPUT)
--AS
----DECLARE @file_id uniqueidentifier ;
--BEGIN

--SET  @file_id=NEWID();
--		INSERT INTO files(stream_id,file_stream,name) VALUES(@file_id,@file_data,@file_name);
--		SELECT stream_id,file_stream.PathName() as unc_path 
--		FROM v_attached_file
--		WHERE stream_id=@file_id;
--		RETURN  

--END
