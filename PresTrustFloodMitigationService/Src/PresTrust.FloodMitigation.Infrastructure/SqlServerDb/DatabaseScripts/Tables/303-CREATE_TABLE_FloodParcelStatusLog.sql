IF OBJECT_ID('[Flood].[FloodParcelStatusLog]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodParcelStatusLog] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelStatusLog];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelStatusLog]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelStatusLog](
	[ApplicationId]							[integer]						NOT NULL,
	[PamsPin]								[varchar](76)					NOT NULL,
	[StatusId]								[integer]						NOT NULL,
	[StatusDate]							[datetime]						NULL,
	[Notes]									[varchar](max)					NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL,
	[LastUpdatedOn]							[datetime]						NOT NULL)
GO

ALTER TABLE [Flood].[FloodParcelStatusLog] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelStatusLog]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

  

 