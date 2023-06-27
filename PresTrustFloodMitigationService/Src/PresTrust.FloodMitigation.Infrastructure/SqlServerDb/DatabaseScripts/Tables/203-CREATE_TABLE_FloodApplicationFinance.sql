 
IF OBJECT_ID('[Flood].[FloodFinance]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodFinance];
	
	ALTER TABLE [Flood].[FloodFinance] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFinance];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFinance]
GO

-- Create Table
CREATE TABLE [Flood].[FloodFinance](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,

	[MatchPercent]							[decimal](18,2)					NOT NULL,
	[TotalProjectCost]						[decimal](18,2)					NOT NULL,
	[GrantRequest]							[decimal](18,2)					NOT NULL,
	[Match]									[decimal](18,2)					NOT NULL,
 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
		
CONSTRAINT [PK_FloodFinance_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodFinance] ADD CONSTRAINT [FK_ApplicationId_FloodFinance]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodFinance] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFinance]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
