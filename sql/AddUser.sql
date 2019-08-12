-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AddUser
	@userId nvarchar(9),
	@firstName nvarchar(20),
	@lastName nvarchar(20),
	@password nvarchar(10),
	@mail nvarchar(50)
AS
BEGIN
    -- Insert statements for procedure here
	INSERT INTO Users VALUES
	(@userId,@firstName,@lastName,@password,@mail)
END
