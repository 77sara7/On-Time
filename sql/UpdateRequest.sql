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
-- exec UpdateRequest 2,'Bank' ,'2019-02-10' ,'2019-02-20',3,12,2,1
-- =============================================
CREATE PROCEDURE UpdateRequest
	@requestId int,
	@requestName nvarchar(30),
	@dateFrom date,
	@dateTo date,
	@day int,
	@hour int,
	@dayInMonth int,
	@frequencyId int
AS
BEGIN
    -- Insert statements for procedure here
	UPDATE Request
	SET requestName=@requestName, dateFrom=@dateFrom, dateTo=@dateTo ,day=@day,hour=@hour,dayInMonth=@dayInMonth,frequencyId=@frequencyId
	WHERE requestId=@requestId
END

