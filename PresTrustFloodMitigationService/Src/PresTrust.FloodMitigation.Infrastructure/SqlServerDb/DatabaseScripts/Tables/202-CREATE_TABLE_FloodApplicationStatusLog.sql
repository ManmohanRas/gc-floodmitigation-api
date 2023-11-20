 
IF OBJECT_ID('[Flood].[FloodApplicationStatusLog]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodApplicationStatusLog] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationStatusLog];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationStatusLog]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationStatusLog](
	[ApplicationId]							[integer]						NOT NULL,
	[StatusId]								[integer]						NOT NULL,
	[StatusDate]							[datetime]						NULL,
	[Notes]									[varchar](max)					NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL,
	[LastUpdatedOn]							[datetime]						NOT NULL)
GO

ALTER TABLE [Flood].[FloodApplicationStatusLog] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationStatusLog]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
