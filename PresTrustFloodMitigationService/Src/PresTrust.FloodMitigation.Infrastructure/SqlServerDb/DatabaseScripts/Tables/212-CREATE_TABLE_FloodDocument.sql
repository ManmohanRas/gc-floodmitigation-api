IF OBJECT_ID('[Flood].[FloodDocument]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodDocument] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodDocument];
		
	ALTER TABLE [Flood].[FloodDocument] DROP CONSTRAINT IF EXISTS  [FK_DocumentTypeId_FloodDocument];

	ALTER TABLE [Flood].[FloodDocument] DROP CONSTRAINT IF EXISTS  [DF_ShowCommittee_FloodDocument];
	
	ALTER TABLE [Flood].[FloodDocument] DROP CONSTRAINT IF EXISTS  [DF_UseInReport_FloodDocument];
	
	ALTER TABLE [Flood].[FloodDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodDocument];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodDocument]
GO

-- Create Table
CREATE TABLE [Flood].[FloodDocument](
	[Id]				[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]		[integer]						NOT NULL,
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
	[LastUpdatedOn]		[dateTime]						NOT NULL,
CONSTRAINT [PK_FloodDocument_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodDocument] ADD CONSTRAINT FK_DocumentTypeId_FloodDocument  FOREIGN KEY (DocumentTypeId) REFERENCES [Flood].FloodDocumentType(Id);
GO 

ALTER TABLE [Flood].[FloodDocument] ADD CONSTRAINT FK_ApplicationId_FloodDocument  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO  

ALTER TABLE [Flood].[FloodDocument] WITH NOCHECK ADD  CONSTRAINT [DF_ShowCommittee_FloodDocument]  DEFAULT (0) FOR [ShowCommittee]
GO  

ALTER TABLE [Flood].[FloodDocument] WITH NOCHECK ADD  CONSTRAINT [DF_UseInReport_FloodDocument]  DEFAULT (0) FOR [UseInReport]
GO  

ALTER TABLE [Flood].[FloodDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
  

 