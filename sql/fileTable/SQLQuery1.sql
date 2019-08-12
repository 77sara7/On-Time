-- =========================================
-- Create FileTable template
-- =========================================
USE [master]
GO 
alter DATABASE ON_TIME_DB
ADD FILEGROUP MyFiles
CONTAINS FILESTREAM 
GO
ALTER DATABASE ON_TIME_DB
ADD FILE
(
NAME=N'File01',
FILENAME=N'D:\File01.ndf'
)
TO FILEGROUP [MyFiles]
GO