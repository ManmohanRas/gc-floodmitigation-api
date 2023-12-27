IF OBJECT_ID('[Flood].[FloodFlapTargetArea]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodFlapTargetArea] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFlapTargetArea];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFlapTargetArea]
GO

-- Create Table
CREATE TABLE [Flood].[FloodFlapTargetArea](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[AgencyId]							    [integer]						NOT NULL,
	[TargetArea]							[varchar](128)					NOT NULL,
	[CreatedDate]                           [datetime]						NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[datetime]						NOT NULL,

CONSTRAINT [PK_FloodFlapTargetArea_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints

ALTER TABLE [Flood].[FloodFlapTargetArea] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFlapTargetArea]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
