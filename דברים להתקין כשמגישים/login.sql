USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [muser]    Script Date: 08/07/2019 12:58:11 ******/
CREATE LOGIN [muser] WITH PASSWORD=N'CscQsk61DG9NZnvJFFh5iksOBa5joRoGQTzDR6FfnZQ=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [muser] DISABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [muser]
GO


