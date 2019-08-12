USE [WebAPI_207618935]
GO
/****** Object:  StoredProcedure [dbo].[SP_Add_customer]    Script Date: 10/02/2019 11:48:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
exec AddRequest 'Bank' ,'2019-02-10' ,'2019-02-20', '206504482' ,3,12,2,1
-- =============================================
CREATE PROCEDURE AddRequest
	@requestName nvarchar(30),
	@dateFrom date,
	@dateTo date,
	@userId nvarchar(9),
	@day int,
	@hour int,
	@dayInMonth int,
	@frequencyId int
AS
BEGIN
    -- Insert statements for procedure here
	INSERT INTO Request
	VALUES (@requestName,@dateFrom,@dateTo,@userId,@day,@hour,@dayInMonth,@frequencyId)
	
END
