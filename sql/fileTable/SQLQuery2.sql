sp_configure 'filestream access level', 2
reconfigure with override
alter DATABASE ON_TIME_DB
ADD FILEGROUP MyFiles
ALTER DATABASE ON_TIME_DB MODIFY FILEGROUP MyFiles DEFAULT

CONTAINS FILESTREAM 