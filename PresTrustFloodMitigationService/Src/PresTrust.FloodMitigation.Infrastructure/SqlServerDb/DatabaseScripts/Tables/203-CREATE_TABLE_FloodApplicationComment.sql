IF OBJECT_ID('[Flood].[FloodApplicationComment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationComment];
	
	ALTER TABLE [Flood].[FloodApplicationComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationComment];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationComment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationComment](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,
	[Comment]								[varchar](4000)					NOT NULL,
	[CommentTypeId]							[smallint]						NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[datetime]						NOT NULL,

CONSTRAINT [PK_FloodApplicationComment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationComment] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationComment]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
