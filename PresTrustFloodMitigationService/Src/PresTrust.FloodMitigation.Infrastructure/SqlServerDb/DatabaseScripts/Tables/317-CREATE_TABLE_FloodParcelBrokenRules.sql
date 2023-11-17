IF OBJECT_ID('[Flood].[FloodParcelBrokenRules]') IS NOT NULL
BEGIN
	-- Drop Constraint
	ALTER TABLE [Flood].[FloodParcelBrokenRules] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelBrokenRules];
	ALTER TABLE [Flood].[FloodParcelBrokenRules] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelBrokenRules];
	DROP INDEX IF EXISTS [IX_FloodParcelBrokenRules_ApplicationId_SectionId] ON [Flood].[FloodParcelBrokenRules];
END
GO



-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelBrokenRules];
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelBrokenRules](
	[ApplicationId]		[integer]						NOT NULL,
	[PamsPin]			[varchar](76)					NOT NULL,
	[SectionId]			[integer]						NOT NULL,
	[Message]			[varchar](1024)					NOT NULL,
	[IsPropertyFlow]	[bit]							NOT NULL,
	[LastUpdatedOn]		[datetime]						NOT NULL,
) ON [PRIMARY]

GO

-- Create a clustered index  
CREATE CLUSTERED INDEX IX_FloodParcelBrokenRules_ApplicationId_SectionId ON [Flood].[FloodParcelBrokenRules] (ApplicationId, SectionId); 
GO
 
-- Create Constraint
ALTER TABLE [Flood].[FloodParcelBrokenRules] ADD CONSTRAINT FK_ApplicationId_FloodParcelBrokenRules  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  

ALTER TABLE [Flood].[FloodParcelBrokenRules] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelBrokenRules]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
