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
-- exec DeleteRequest 2
-- =============================================
CREATE PROCEDURE DeleteRequest
	@requestId int
AS
BEGIN
    -- Insert statements for procedure here
	UPDATE Request
	SET dateTo=DATEADD(dd,-1,GETDATE())
	WHERE requestId=@requestId
END
