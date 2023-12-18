IF OBJECT_ID('[Flood].[FloodAgencyFlapComment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodAgencyFlapComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodAgencyFlapComment];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodAgencyFlapComment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodAgencyFlapComment](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[AgencyId]							    [integer]						NOT NULL,
	[Comment]								[varchar](4000)					NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[datetime]						NOT NULL,

CONSTRAINT [PK_FloodAgencyFlapComment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints

ALTER TABLE [Flood].[FloodAgencyFlapComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodAgencyFlapComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
