USE [GraniteExpress4]
GO

/****** Object:  StoredProcedure [dbo].[select_journal]    Script Date: 2024-05-10 14:02:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[select_journal] 
	@bdate datetime2, 
	@edate datetime2
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select jm.DocumentNo,jm.DocumentDate, jd.AccountId, jd.ExchangeRate,jd.CurrencyAmount from GenJournal jm join GenJournalDetails jd on jm.JournalId=jd.JournalId

where jm.DocumentDate between @bdate and @edate 
	
END
GO





/****** Object:  StoredProcedure [dbo].[AddJournal]    Script Date: 2024-05-10 14:02:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddJournal] 
	@TemplateId int,
	@DocumentDate datetime2, 
	@JournalDescription nvarchar(max),
	@DocumentTypeId int,
	@ClientId int,
	@DocumentNo nvarchar(max),
	@UserId nvarchar(450),
	@IsDelete bit
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @JournalId int
	INSERT INTO GenJournal (TemplateId,JournalDescription,DocumentTypeId,ClientId,DocumentNo,DocumentDate,UserId,IsDelete) VALUES (@TemplateId,@JournalDescription,@DocumentTypeId,@ClientId,@DocumentNo,@DocumentDate,@UserId,0) set @JournalId = SCOPE_IDENTITY();
	SELECT * from GenJournal where JournalId = @JournalId 	
END
GO




