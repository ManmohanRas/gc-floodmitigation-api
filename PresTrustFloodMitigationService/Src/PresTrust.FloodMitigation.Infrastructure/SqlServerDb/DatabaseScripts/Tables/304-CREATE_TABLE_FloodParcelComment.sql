IF OBJECT_ID('[Flood].[FloodParcelComment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelComment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelComment];
	
	ALTER TABLE [Flood].[FloodParcelComment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelComment];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelComment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelComment](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,
	[PamsPin]								[varchar](76)					NOT NULL,
	[Comment]								[varchar](4000)					NOT NULL,
	[CommentTypeId]							[smallint]						NOT NULL,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[datetime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodParcelComment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodParcelComment] ADD CONSTRAINT [FK_ApplicationId_FloodParcelComment]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodParcelComment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelComment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
