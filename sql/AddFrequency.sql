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
-- exec AddFrequency 'Day'
-- =============================================
CREATE PROCEDURE AddFrequency
	@frequencyDescription nvarchar(20)
AS
BEGIN
    -- Insert statements for procedure here
	INSERT INTO Frequency
	VALUES (@frequencyDescription)
	
END