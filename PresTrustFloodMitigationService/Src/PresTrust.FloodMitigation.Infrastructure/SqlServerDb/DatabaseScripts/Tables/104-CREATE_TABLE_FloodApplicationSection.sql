
IF OBJECT_ID('[Flood].[FloodApplicationSection]') IS NOT NULL
BEGIN
	-- Drop Constraint
	ALTER TABLE [Flood].[FloodApplicationSection] DROP CONSTRAINT IF EXISTS  [DF_FloodApplicationSection_Description]
END
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationSection];
GO
 
-- Create Table
CREATE TABLE [Flood].[FloodApplicationSection](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL,		
CONSTRAINT [PK_FloodApplicationSection_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationSection] WITH NOCHECK ADD  CONSTRAINT [DF_FloodApplicationSection_Description]  DEFAULT ('') FOR [Description]
GO 

 

