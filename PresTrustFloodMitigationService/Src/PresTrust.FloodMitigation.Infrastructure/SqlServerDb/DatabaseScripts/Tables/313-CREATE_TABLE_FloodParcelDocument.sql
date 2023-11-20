IF OBJECT_ID('[Flood].[FloodParcelDocument]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelDocument];
		
	ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodParcelDocument];

	ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [DF_ShowCommittee_FloodParcelDocument];
	
	ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [DF_UseInReport_FloodParcelDocument];
	
	ALTER TABLE [Flood].[FloodParcelDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelDocument];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelDocument]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelDocument](
	[Id]				[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]		[integer]						NOT NULL,
	[PamsPin]			[varchar](76)					NOT NULL,
	[DocumentTypeId]	[smallint]						NOT NULL,
	[FileName]			[varchar](128)					NOT NULL,
	[Title]				[varchar](128)					NOT NULL,
	[Description]		[varchar](256)					NULL	,
	[ShowCommittee]		[bit]							NOT NULL,
	[UseInReport]		[bit]							NOT NULL,
	[HardCopy]			[bit]							NOT NULL,
	[Approved]			[bit]							NOT NULL,
	[ReviewComment]		[varchar](2000)					NULL	,
	[LastUpdatedBy]		[varchar](128)					NULL	,
	[LastUpdatedOn]		[datetime]						NOT NULL,
CONSTRAINT [PK_FloodParcelDocument_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodParcelDocument] ADD CONSTRAINT FK_DocumentTypeId_FloodParcelDocument  FOREIGN KEY (DocumentTypeId) REFERENCES [Flood].FloodParcelDocumentType(Id);
GO 

ALTER TABLE [Flood].[FloodParcelDocument] ADD CONSTRAINT FK_ApplicationId_FloodParcelDocument  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  

ALTER TABLE [Flood].[FloodParcelDocument] WITH NOCHECK ADD  CONSTRAINT [DF_ShowCommittee_FloodParcelDocument]  DEFAULT (0) FOR [ShowCommittee]
GO  

ALTER TABLE [Flood].[FloodParcelDocument] WITH NOCHECK ADD  CONSTRAINT [DF_UseInReport_FloodParcelDocument]  DEFAULT (0) FOR [UseInReport]
GO  

ALTER TABLE [Flood].[FloodParcelDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
  

 