 
IF OBJECT_ID('[Flood].[FloodApplicationFinanceFund]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinanceFund];
	
	ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_FundingSourceTypeId_FloodApplicationFinanceFund];

	ALTER TABLE [Flood].[FloodApplicationFinanceFund] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFinanceFund];
END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationFinanceFund]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationFinanceFund](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,

	[FundingSourceTypeId]                   [integer]                       NOT NULL,
	[Title]									[varchar](256)				    NULL    ,
	[Amount]								[decimal](18,2)					NOT NULL,
	[AwardDate]								[DateTime]						NULL    ,

	[LastUpdatedBy]							[varchar](128)					NULL,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
		
CONSTRAINT [PK_FloodApplicationFinanceFund_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationFinanceFund] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationFinanceFund]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodApplicationFinanceFund] ADD CONSTRAINT [FK_FundingSourceTypeId_FloodApplicationFinanceFund]  FOREIGN KEY (FundingSourceTypeId) REFERENCES [Flood].FloodApplicationFundingSourceType(Id);
GO

ALTER TABLE [Flood].[FloodApplicationFinanceFund] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFinanceFund]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
