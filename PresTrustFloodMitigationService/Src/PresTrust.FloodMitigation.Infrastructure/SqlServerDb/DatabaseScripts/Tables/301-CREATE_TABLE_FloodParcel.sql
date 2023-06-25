IF OBJECT_ID('[Flood].[FloodParcel]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [FK_StatusId_FloodParcel];

	ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationTypeId_FloodParcel];
	
	ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationSubTypeId_FloodParcel];
	
	ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcel];

	ALTER TABLE [Flood].[FloodParcel] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodParcel];

END;
GO
 
 
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcel]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcel](
	[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]				[integer]						NOT NULL,
	[Title]						[varchar](256)					NOT NULL,
	[AgencyId]					[integer]						NOT NULL,
	[ApplicationTypeId]			[smallint]						NOT NULL,
	[ApplicationSubTypeId]		[smallint]						NOT NULL,
	[StatusId]					[smallint]						NOT NULL,
	[ExpirationDate]			[DateTime]						NULL	,
	[LastUpdatedBy]				[varchar](128)					NULL	,
	[LastUpdatedOn]				[DateTime]						NOT NULL,
	[IsActive]					[bit]							NOT NULL,
	
CONSTRAINT [PK_FloodParcel_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodParcel] ADD CONSTRAINT FK_StatusId_FloodParcel  FOREIGN KEY (StatusId) REFERENCES [Flood].FloodParcelStatus(Id);
GO 

ALTER TABLE [Flood].[FloodParcel] ADD CONSTRAINT FK_ApplicationTypeId_FloodParcel  FOREIGN KEY (ApplicationTypeId) REFERENCES [Flood].FloodParcelType(Id);
GO 

ALTER TABLE [Flood].[FloodParcel] ADD CONSTRAINT [FK_ApplicationSubTypeId_FloodParcel]  FOREIGN KEY (ApplicationSubTypeId) REFERENCES [Flood].FloodParcelSubType(Id);
GO 

ALTER TABLE [Flood].[FloodParcel] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcel]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

ALTER TABLE [Flood].[FloodParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodParcel]  DEFAULT (1) FOR [IsActive]
GO  
