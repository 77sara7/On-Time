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
exec GetUserRequests '206504482' 
-- =============================================
CREATE PROCEDURE GetUserRequests	
	@userId nvarchar(9)
AS
BEGIN
    -- Insert statements for procedure here
	SELECT *
	FROM Request
	WHERE userId like @userId
END
