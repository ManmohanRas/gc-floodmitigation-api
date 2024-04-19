IF OBJECT_ID('[Flood].[FloodEmailTemplate]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodEmailTemplate] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodEmailTemplate];

	ALTER TABLE [Flood].[FloodEmailTemplate] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodEmailTemplate];

END
GO	

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodEmailTemplate]
GO

CREATE TABLE [Flood].[FloodEmailTemplate](
[Id] [smallint]  NOT NULL,
[TemplateCode] [varchar](128) NOT NULL,
[Title] [varchar](256) NOT NULL,
[Subject] [varchar](512) NULL,
[Description] [varchar](max) NOT NULL,
[IsActive] [bit] NULL,
[LastUpdatedBy] [varchar](128) NULL,
[LastUpdatedOn] [datetime] NULL,

CONSTRAINT [PK_FloodEmailTemplate_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodEmailTemplate] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodEmailTemplate]  DEFAULT (1) FOR [IsActive]
GO

ALTER TABLE [Flood].[FloodEmailTemplate] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodEmailTemplate]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
