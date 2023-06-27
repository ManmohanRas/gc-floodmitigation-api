IF OBJECT_ID('[Flood].[FloodSignature]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodSignature] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodSignature];
		
	ALTER TABLE [Flood].[FloodSignature] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodSignature];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodSignature]
GO

-- Create Table
CREATE TABLE [Flood].[FloodSignature](
	[Id]							[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]					[integer]						NOT NULL,
	[Designation]					[varchar](128)					NULL	,
	[Title]							[varchar](128)					NULL	, 
	[SignedOn]						[DateTime]						NULL	, 
	[SignatureType]					[varchar](128)					NOT NULL,
	[LastUpdatedBy]					[varchar](128)					NULL	, 
	[LastUpdatedOn]					[DateTime]						NOT NULL, 
CONSTRAINT [PK_FloodSignature_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodSignature] ADD CONSTRAINT [FK_ApplicationId_FloodSignature]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodSignature] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodSignature]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

