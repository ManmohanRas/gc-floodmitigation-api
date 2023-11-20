IF OBJECT_ID('[Flood].[FloodApplicationDocument]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationDocument];
		
	ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodApplicationDocument];

	ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [DF_ShowCommittee_FloodApplicationDocument];
	
	ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [DF_UseInReport_FloodApplicationDocument];
	
	ALTER TABLE [Flood].[FloodApplicationDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationDocument];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationDocument]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationDocument](
	[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]				[integer]						NOT NULL,
	[DocumentTypeId]			[smallint]						NOT NULL,
	[OtherFundingSourceId]		[integer]						NULL,
	[FileName]					[varchar](128)					NOT NULL,
	[Title]						[varchar](128)					NOT NULL,
	[Description]				[varchar](256)					NULL,
	[ShowCommittee]				[bit]							NOT NULL,
	[UseInReport]				[bit]							NOT NULL,
	[HardCopy]					[bit]							NOT NULL,
	[Approved]					[bit]							NOT NULL,
	[ReviewComment]				[varchar](2000)					NULL,
	[LastUpdatedBy]				[varchar](128)					NULL,
	[LastUpdatedOn]				[datetime]						NOT NULL,
CONSTRAINT [PK_FloodApplicationDocument_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationDocument] ADD CONSTRAINT FK_DocumentTypeId_FloodApplicationDocument  FOREIGN KEY (DocumentTypeId) REFERENCES [Flood].FloodApplicationDocumentType(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationDocument] ADD CONSTRAINT FK_ApplicationId_FloodApplicationDocument  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  

ALTER TABLE [Flood].[FloodApplicationDocument] WITH NOCHECK ADD  CONSTRAINT [DF_ShowCommittee_FloodApplicationDocument]  DEFAULT (0) FOR [ShowCommittee]
GO  

ALTER TABLE [Flood].[FloodApplicationDocument] WITH NOCHECK ADD  CONSTRAINT [DF_UseInReport_FloodApplicationDocument]  DEFAULT (0) FOR [UseInReport]
GO  

ALTER TABLE [Flood].[FloodApplicationDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
  

 