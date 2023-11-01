 
IF OBJECT_ID('[Flood].[FloodApplicationStatusLog]') IS NOT NULL
BEGIN
	-- Drop Constraints
	--ALTER TABLE [Flood].[FloodApplicationStatusLog] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationStatusLog];
	
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
	[StatusDate]							[DateTime]						NULL,
	[Notes]									[varchar](max)					NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
CONSTRAINT [PK_FloodApplicationStatusLog_Id] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC, [StatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
--ALTER TABLE [Flood].[FloodApplicationStatusLog] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationStatusLog]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
--GO 

ALTER TABLE [Flood].[FloodApplicationStatusLog] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationStatusLog]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
