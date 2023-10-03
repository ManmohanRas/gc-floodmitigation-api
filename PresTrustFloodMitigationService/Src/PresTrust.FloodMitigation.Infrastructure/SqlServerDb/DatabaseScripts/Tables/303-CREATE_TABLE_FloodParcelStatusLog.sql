IF OBJECT_ID('[Flood].[FloodParcelStatusLog]') IS NOT NULL
BEGIN
	-- Drop Constraints
	--ALTER TABLE [Flood].[FloodParcelStatusLog] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelStatusLog];
	
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
	[StatusDate]							[date]							NULL	,
	[Notes]									[varchar](max)					NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
CONSTRAINT [PK_FloodParcelStatusLog_Id] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC, [StatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
--ALTER TABLE [Flood].[FloodParcelStatusLog] ADD CONSTRAINT [FK_ApplicationId_FloodParcelStatusLog]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
--GO 

ALTER TABLE [Flood].[FloodParcelStatusLog] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelStatusLog]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

  

 