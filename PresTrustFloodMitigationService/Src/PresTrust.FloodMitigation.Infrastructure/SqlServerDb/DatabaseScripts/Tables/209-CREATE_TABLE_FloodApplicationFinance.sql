IF OBJECT_ID('[Flood].[FloodApplicationFinance]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFinance];
	
	ALTER TABLE [Flood].[FloodApplicationFinance] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFinance];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationFinance]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationFinance](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,

	[MatchPercent]							[decimal](18,2)					NOT NULL,
 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
		
CONSTRAINT [PK_FloodApplicationFinance_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationFinance] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationFinance]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationFinance] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFinance]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
