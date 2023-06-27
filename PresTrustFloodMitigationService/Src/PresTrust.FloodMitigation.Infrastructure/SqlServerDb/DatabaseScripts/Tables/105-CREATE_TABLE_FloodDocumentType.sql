
IF OBJECT_ID('[Flood].[FloodDocumentType]') IS NOT NULL
BEGIN
	-- Drop Constraint
	ALTER TABLE [Flood].[FloodDocumentType] DROP CONSTRAINT IF EXISTS  [FK_SectionId_FloodDocumentType]
END
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodDocumentType];
GO

-- Create Table
CREATE TABLE [Flood].[FloodDocumentType](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL	,
	[SectionId]			[smallint]			NOT NULL
CONSTRAINT [PK_FloodDocumentType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodDocumentType] ADD CONSTRAINT [FK_SectionId_FloodDocumentType]  FOREIGN KEY (SectionId) REFERENCES [Flood].FloodApplicationSection(Id);
GO 
 


 
  

