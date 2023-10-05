-- Drop Constraints
IF OBJECT_ID('[Flood].[FloodParcelDocumentType]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodParcelDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodParcelDocumentType]
END
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelDocumentType];
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelDocumentType](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL	,
	[SectionId]			[smallint]			NOT NULL
CONSTRAINT [PK_FloodParcelDocumentType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodParcelDocumentType] ADD CONSTRAINT [FK_SectionId_FloodParcelDocumentType]  FOREIGN KEY (SectionId) REFERENCES [Flood].FloodApplicationSection(Id);
GO
