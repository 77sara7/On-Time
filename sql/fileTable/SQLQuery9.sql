CREATE PROCEDURE attached_file(@file_name nvarchar(255),@file_data varbinary(max))
AS
DECLARE @file_id uniqueidentifier ;
BEGIN
SET  @file_id=NEWID();
		INSERT INTO files(stream_id,file_stream,name) VALUES(@file_id,@file_data,@file_name);
		--SELECT stream_id,file_stream.PathNAme() as unc_path 
		--FROM files
		--WHERE stream_id=@file_id;
END