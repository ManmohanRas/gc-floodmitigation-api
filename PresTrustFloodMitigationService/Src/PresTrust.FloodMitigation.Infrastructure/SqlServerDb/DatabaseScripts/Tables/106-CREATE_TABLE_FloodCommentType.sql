-- Drop Constraints
IF OBJECT_ID('[Flood].[FloodCommentType]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodCommentType] DROP CONSTRAINT IF EXISTS  [DF_FloodCommentType_Description];
	
	ALTER TABLE [Flood].[FloodCommentType] DROP CONSTRAINT IF EXISTS  [DF_FloodCommentType_IsActive];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodCommentType];
GO

-- Create Table
CREATE TABLE [Flood].[FloodCommentType](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL,		
	[IsActive]			[bit]				NULL,
CONSTRAINT [PK_FloodCommentType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodCommentType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodCommentType_Description]  DEFAULT ('') FOR [Description]
GO 
 
ALTER TABLE [Flood].[FloodCommentType] WITH NOCHECK ADD  CONSTRAINT [DF_FloodCommentType_IsActive]  DEFAULT (1) FOR [IsActive]
GO
