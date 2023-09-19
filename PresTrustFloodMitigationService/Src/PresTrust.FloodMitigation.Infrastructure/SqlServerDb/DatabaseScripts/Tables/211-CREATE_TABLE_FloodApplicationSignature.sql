IF OBJECT_ID('[Flood].[FloodApplicationSignature]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationSignature] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationSignature];
		
	ALTER TABLE [Flood].[FloodApplicationSignature] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationSignature];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationSignature]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationSignature](
	[Id]							[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]					[integer]						NOT NULL,
	[Designation]					[varchar](128)					NULL	,
	[Title]							[varchar](128)					NULL	, 
	[SignedOn]						[DateTime]						NULL	, 
	[SignatureType]					[varchar](128)					NOT NULL,
	[LastUpdatedBy]					[varchar](128)					NULL	, 
	[LastUpdatedOn]					[DateTime]						NOT NULL, 
CONSTRAINT [PK_FloodApplicationSignature_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationSignature] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationSignature]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationSignature] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationSignature]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

