-- Drop Constraints
IF OBJECT_ID('[Flood].[FloodApplicationCommentType]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodApplicationCommentType] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationCommentType_Description];
	
	ALTER TABLE [Flood].[FloodApplicationCommentType] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationCommentType_IsActive];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationCommentType];
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationCommentType](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL,		
	[IsActive]			[bit]				NULL,
CONSTRAINT [PK_FloodApplicationCommentType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationCommentType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationCommentType_Description]  DEFAULT ('') FOR [Description]
GO 
 
ALTER TABLE [Flood].[FloodApplicationCommentType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationCommentType_IsActive]  DEFAULT (1) FOR [IsActive]
GO
