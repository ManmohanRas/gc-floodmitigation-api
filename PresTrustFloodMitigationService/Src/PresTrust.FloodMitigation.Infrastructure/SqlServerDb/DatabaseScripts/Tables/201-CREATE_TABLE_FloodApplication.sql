IF OBJECT_ID('[Flood].[FloodApplication]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [FK_StatusId_FloodApplication];

	ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [FK_ApplicationTypeId_FloodApplication];
	
	ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [FK_ApplicationSubTypeId_FloodApplication];
	
	ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [DF_CreatedByProgramAdmin_FloodApplication];

	ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplication];

	ALTER TABLE [Flood].[FloodApplication] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodApplication];

END;
GO
 
 
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplication]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplication](
	[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
	[Title]						[varchar](256)					NOT NULL,
	[AgencyId]					[integer]						NOT NULL,
	[ApplicationTypeId]			[smallint]						NOT NULL,
	[ApplicationSubTypeId]		[smallint]						NOT NULL,
	[StatusId]					[smallint]						NOT NULL,
	[ExpirationDate]			[DateTime]						NULL	,
	[CreatedByProgramAdmin]		[bit]							NOT NULL,
	[LastUpdatedBy]				[varchar](128)					NULL	,
	[LastUpdatedOn]				[DateTime]						NOT NULL,
	[IsActive]					[bit]							NOT NULL,
	
CONSTRAINT [PK_FloodApplication_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplication] ADD CONSTRAINT FK_StatusId_FloodApplication  FOREIGN KEY (StatusId) REFERENCES [Flood].FloodApplicationStatus(Id);
GO 

ALTER TABLE [Flood].[FloodApplication] ADD CONSTRAINT FK_ApplicationTypeId_FloodApplication  FOREIGN KEY (ApplicationTypeId) REFERENCES [Flood].FloodApplicationType(Id);
GO 

ALTER TABLE [Flood].[FloodApplication] ADD CONSTRAINT [FK_ApplicationSubTypeId_FloodApplication]  FOREIGN KEY (ApplicationSubTypeId) REFERENCES [Flood].FloodApplicationSubType(Id);
GO 

ALTER TABLE [Flood].[FloodApplication] WITH NOCHECK ADD  CONSTRAINT [DF_CreatedByProgramAdmin_FloodApplication]  DEFAULT (0) FOR [CreatedByProgramAdmin]
GO

ALTER TABLE [Flood].[FloodApplication] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplication]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

ALTER TABLE [Flood].[FloodApplication] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodApplication]  DEFAULT (1) FOR [IsActive]
GO
