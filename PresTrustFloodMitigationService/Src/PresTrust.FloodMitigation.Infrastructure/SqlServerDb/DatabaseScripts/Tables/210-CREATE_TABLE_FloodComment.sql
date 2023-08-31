IF OBJECT_ID('[Flood].[FloodComment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodComment];
	
	ALTER TABLE [Flood].[FloodComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodComment];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodComment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodComment](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,
	[Comment]								[varchar](4000)					NOT NULL,
	[CommentTypeId]							[smallint]						NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodComment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodComment] ADD CONSTRAINT [FK_ApplicationId_FloodComment]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
