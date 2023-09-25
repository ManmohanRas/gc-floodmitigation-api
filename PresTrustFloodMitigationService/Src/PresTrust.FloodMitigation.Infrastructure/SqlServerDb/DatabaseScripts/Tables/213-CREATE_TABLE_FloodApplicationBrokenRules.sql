IF OBJECT_ID('[Flood].[FloodApplicationBrokenRules]') IS NOT NULL
BEGIN
	-- Drop Constraint
	ALTER TABLE [Flood].[FloodApplicationBrokenRules] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationBrokenRules];
	ALTER TABLE [Flood].[FloodApplicationBrokenRules] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationBrokenRules];
	DROP INDEX IF EXISTS [IX_FloodApplicationBrokenRules_ApplicationId_SectionId] ON [Flood].[FloodApplicationBrokenRules];
END
GO



-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationBrokenRules];
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationBrokenRules](
	[ApplicationId]		[integer]						NOT NULL,
	[SectionId]			[integer]						NOT NULL,
	[Message]			[varchar](1024)					NOT NULL,
	[IsApplicantFlow]	[bit]							NOT NULL,
	[LastUpdatedOn]		[datetime]						NOT NULL,
) ON [PRIMARY]

GO

-- Create a clustered index  
CREATE CLUSTERED INDEX IX_FloodApplicationBrokenRules_ApplicationId_SectionId ON [Flood].[FloodApplicationBrokenRules] (ApplicationId, SectionId); 
GO
 
-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationBrokenRules] ADD CONSTRAINT FK_ApplicationId_FloodApplicationBrokenRules  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  

ALTER TABLE [Flood].[FloodApplicationBrokenRules] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationBrokenRules]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
