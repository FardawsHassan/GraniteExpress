USE [GraniteExpress1]
GO

INSERT INTO [dbo].[Clientype]
           ([ClientType])
     VALUES
           ('european')
GO

INSERT INTO [dbo].[Clientype]
           ([ClientType])
     VALUES
           ('american')
GO




INSERT INTO [dbo].[RefClient]
           ([ClientTypeId]
           ,[ClientFirstName]
           ,[ClientLastName]
           ,[ClientCode])
     VALUES
           (1
           ,'Fardaws'
           ,'Hassan'
           ,'FA12')
GO

INSERT INTO [dbo].[RefClient]
           ([ClientTypeId]
           ,[ClientFirstName]
           ,[ClientLastName]
           ,[ClientCode])
     VALUES
           (1
           ,'Altaf'
           ,'Hossain'
           ,'AH33')
GO



INSERT INTO [dbo].[RefCurrency]
           ([CurrencyCode])
     VALUES
           ('MNT')
		   
INSERT INTO [dbo].[RefCurrency]
           ([CurrencyCode])
     VALUES
           ('BDT')
		   		   
INSERT INTO [dbo].[RefCurrency]
           ([CurrencyCode])
     VALUES
           ('USD')

GO




INSERT INTO [dbo].[RefDocumentType]
           ([DocumentName]
           ,[DocumentCode]
           ,[DocumentMaxValue])
     VALUES
           ('Financial',
           1211121,
           111)
		   
INSERT INTO [dbo].[RefDocumentType]
           ([DocumentName]
           ,[DocumentCode]
           ,[DocumentMaxValue])
     VALUES
           ('Tax',
           1211545,
           121)
GO




INSERT INTO [dbo].[RefAccountType]
           ([AccountTypeCode]
           ,[AccountTypeName]
           ,[isDebit]
           ,[BalanceId]
           ,[CashId]
           ,[IncomeId]
           ,[EquityId])
     VALUES
           (101120
           ,'Purchase Account'
           ,1
		   ,null,null,null,null)

INSERT INTO [dbo].[RefAccountType]
           ([AccountTypeCode]
           ,[AccountTypeName]
           ,[isDebit]
           ,[BalanceId]
           ,[CashId]
           ,[IncomeId]
           ,[EquityId])
     VALUES
           (101120
           ,'Assets Account'
           ,0
		   ,null,null,null,null)

INSERT INTO [dbo].[RefAccountType]
           ([AccountTypeCode]
           ,[AccountTypeName]
           ,[isDebit]
           ,[BalanceId]
           ,[CashId]
           ,[IncomeId]
           ,[EquityId])
     VALUES
           (101120
           ,'Sells Account'
           ,1
		   ,null,null,null,null)

GO




INSERT INTO [dbo].[RefTemplate]
           ([TemplateName]
           ,[isActive])
     VALUES
           ('Template_1'
           ,1)

INSERT INTO [dbo].[RefTemplate]
           ([TemplateName]
           ,[isActive])
     VALUES
           ('Template_2'
           ,1)

INSERT INTO [dbo].[RefTemplate]
           ([TemplateName]
           ,[isActive])
     VALUES
           ('Template_3'
           ,1)

GO




INSERT INTO [dbo].[RefAccount]
           ([AccountCode]
           ,[AccountTypeId]
           ,[CurrencyId]
           ,[AccountName]
           ,[IsActive])
     VALUES
           ('C5654'
           ,1
           ,1
           ,'Cash in Hand'
           ,1)
GO

INSERT INTO [dbo].[RefAccount]
           ([AccountCode]
           ,[AccountTypeId]
           ,[CurrencyId]
           ,[AccountName]
           ,[IsActive])
     VALUES
           ('B3232'
           ,2
           ,2
           ,'Bank'
           ,2)
GO



INSERT INTO [dbo].[RefTemplateDetails]
           ([TemplateId]
           ,[AccountId]
           ,[IsDebit]
           ,[ClientId])
     VALUES
           (1
           ,1
           ,1
           ,1)
GO

INSERT INTO [dbo].[RefTemplateDetails]
           ([TemplateId]
           ,[AccountId]
           ,[IsDebit]
           ,[ClientId])
     VALUES
           (2
           ,2
           ,0
           ,2)
GO

INSERT INTO [dbo].[RefTemplateDetails]
           ([TemplateId]
           ,[AccountId]
           ,[IsDebit]
           ,[ClientId])
     VALUES
           (1
           ,2
           ,0
           ,2)
GO


INSERT INTO [dbo].[RefTemplateDetails]
           ([TemplateId]
           ,[AccountId]
           ,[IsDebit]
           ,[ClientId])
     VALUES
           (1
           ,1
           ,1
           ,2)
GO




INSERT INTO [dbo].[RefCashFlow]
           ([OrderId]
           ,[CashFlowDescription]
           ,[IsVisible])
     VALUES
           (null
           ,'Addition to Cash'
           ,1)
GO

INSERT INTO [dbo].[RefCashFlow]
           ([OrderId]
           ,[CashFlowDescription]
           ,[IsVisible])
     VALUES
           (null
           ,'Substraction to Cash'
           ,1)
GO

INSERT INTO [dbo].[RefCashFlow]
           ([OrderId]
           ,[CashFlowDescription]
           ,[IsVisible])
     VALUES
           (null
           ,'Net Cash form operations'
           ,1)
GO

INSERT INTO [dbo].[RefCashFlow]
           ([OrderId]
           ,[CashFlowDescription]
           ,[IsVisible])
     VALUES
           (null
           ,'Investment'
           ,1)
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
	@TemplateId int = NULL,
	@DocumentDate datetime2, 
	@JournalDescription nvarchar(max),
	@DocumentTypeId int,
	@ClientId int,
	@DocumentNo nvarchar(max),
	@UserId nvarchar(450)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @JournalId int
	INSERT INTO GenJournal (TemplateId,JournalDescription,DocumentTypeId,ClientId,DocumentNo,DocumentDate,UserId,IsDelete) VALUES (@TemplateId,@JournalDescription,@DocumentTypeId,@ClientId,@DocumentNo,@DocumentDate,@UserId,0) set @JournalId = SCOPE_IDENTITY();
	SELECT * from GenJournal where JournalId = @JournalId 	
END









