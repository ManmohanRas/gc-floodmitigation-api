 
IF OBJECT_ID('[Flood].[FloodFinanceFund]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodFinanceFund];
	
	ALTER TABLE [Flood].[FloodFinanceFund] DROP CONSTRAINT IF EXISTS  [FK_FundingSourceTypeId_FloodFinanceFund];

	ALTER TABLE [Flood].[FloodFinanceFund] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFinanceFund];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFinanceFund]
GO

-- Create Table
CREATE TABLE [Flood].[FloodFinanceFund](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,

	[FundingSourceTypeId]                   [integer]                       NOT NULL,
	[Title]									[varchar](256)					NOT NULL,
	[Amount]								[decimal](18,2)					NOT NULL,
	[AwardDate]								[date]							NOT NULL,

	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
		
CONSTRAINT [PK_FloodFinanceFund_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodFinanceFund] ADD CONSTRAINT [FK_ApplicationId_FloodFinanceFund]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodFinanceFund] ADD CONSTRAINT [FK_FundingSourceTypeId_FloodFinanceFund]  FOREIGN KEY (FundingSourceTypeId) REFERENCES [Flood].FloodFundingSourceType(Id);
GO

ALTER TABLE [Flood].[FloodFinanceFund] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFinanceFund]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
