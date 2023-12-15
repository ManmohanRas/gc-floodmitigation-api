IF OBJECT_ID('[Flood].[FloodFlapDocument]') IS NOT NULL
BEGIN
	-- Drop Constraints
	
	ALTER TABLE [Flood].[FloodFlapDocument] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodFlapDocument];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodFlapDocument]
GO

-- Create Table
CREATE TABLE [Flood].[FloodFlapDocument](
	[Id]						[integer] 		IDENTITY(1,1)	NOT NULL,
	[AgencyId]				    [integer]						NOT NULL,
	[DocumentTypeId]			[smallint]						NOT NULL,
	[FileName]					[varchar](128)					NOT NULL,
	[Title]						[varchar](128)					NOT NULL,
	[LastUpdatedBy]				[varchar](128)					NULL,
	[LastUpdatedOn]				[datetime]						NOT NULL,
CONSTRAINT [PK_FloodFlapDocument_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraint

ALTER TABLE [Flood].[FloodFlapDocument] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodFlapDocument]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
  

 