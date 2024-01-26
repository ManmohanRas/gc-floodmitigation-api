IF OBJECT_ID('[Flood].[FloodMunicipalComment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodMunicipalComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodMunicipalComment];
END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodMunicipalComment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodMunicipalComment](
	[Id]					[integer] 			IDENTITY(1,1)	        NOT NULL,
	[AgencyId]				[integer]									NOT NULL,
	[Comment]				[varchar](4000)				                NULL,												
	[LastUpdatedBy]			[varchar](128)								NOT NULL,
	[LastUpdatedOn]			[datetime]									NOT NULL,
CONSTRAINT [PK_FloodMunicipalComment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodMunicipalComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodMunicipalComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
