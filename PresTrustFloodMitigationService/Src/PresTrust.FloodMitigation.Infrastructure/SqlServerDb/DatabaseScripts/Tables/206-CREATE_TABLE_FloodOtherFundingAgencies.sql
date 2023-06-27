 
IF OBJECT_ID('[Flood].[FloodFundingAgencies]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodFundingAgencies] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodFundingAgencies];
	
	ALTER TABLE [Flood].[FloodFundingAgencies] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFundingAgencies];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFundingAgencies]
GO

-- Create Table
CREATE TABLE [Flood].[FloodFundingAgencies](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,
		
	[AgencyName]							[varchar](256)					NOT NULL,
	[Status]								[varchar](50)					NOT NULL,
	[ApprovedDate]							[date]							NULL	,
 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodFundingAgencies_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodFundingAgencies] ADD CONSTRAINT [FK_ApplicationId_FloodFundingAgencies]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodFundingAgencies] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFundingAgencies]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

  

 