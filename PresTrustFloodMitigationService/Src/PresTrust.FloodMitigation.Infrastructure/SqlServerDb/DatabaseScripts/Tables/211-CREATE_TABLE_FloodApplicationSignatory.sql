IF OBJECT_ID('[Flood].[FloodApplicationSignatory]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationSignatory] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationSignatory];
		
	ALTER TABLE [Flood].[FloodApplicationSignatory] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationSignatory];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationSignatory]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationSignatory](
	[Id]							[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]					[integer]						NOT NULL,
	[Designation]					[varchar](128)					NULL	,
	[Title]							[varchar](128)					NULL	, 
	[SignedOn]						[datetime]						NULL	, 
	[SignatoryType]					[varchar](128)					NOT NULL,
	[LastUpdatedBy]					[varchar](128)					NULL	, 
	[LastUpdatedOn]					[datetime]						NOT NULL, 
CONSTRAINT [PK_FloodApplicationSignatory_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationSignatory] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationSignatory]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationSignatory] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationSignatory]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

