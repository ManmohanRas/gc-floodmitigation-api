-- Drop Constraints
IF OBJECT_ID('[Flood].[FloodApplicationDocumentType]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodApplicationDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodApplicationDocumentType]
END
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationDocumentType];
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationDocumentType](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL	,
	[SectionId]			[smallint]			NOT NULL
CONSTRAINT [PK_FloodApplicationDocumentType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationDocumentType] ADD CONSTRAINT [FK_SectionId_FloodApplicationDocumentType]  FOREIGN KEY (SectionId) REFERENCES [Flood].FloodApplicationSection(Id);
GO
