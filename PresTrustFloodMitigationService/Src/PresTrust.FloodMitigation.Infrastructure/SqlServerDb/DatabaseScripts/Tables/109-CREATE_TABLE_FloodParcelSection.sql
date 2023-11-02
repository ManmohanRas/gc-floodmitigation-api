-- Drop Constraints
IF OBJECT_ID('[Flood].[FloodParcelSection]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodParcelSection] DROP CONSTRAINT IF EXISTS  [DF_FloodParcelSection_Description]
END
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelSection];
GO
 
-- Create Table
CREATE TABLE [Flood].[FloodParcelSection](
	[Id]				[smallint] 			NOT NULL,
	[Title]				[varchar](128)		NOT NULL,
	[Description]		[varchar](512)		NULL,		
CONSTRAINT [PK_FloodParcelSection_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint
ALTER TABLE [Flood].[FloodParcelSection] WITH NOCHECK ADD  CONSTRAINT [DF_FloodParcelSection_Description]  DEFAULT ('') FOR [Description]
GO
