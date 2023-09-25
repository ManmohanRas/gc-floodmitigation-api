IF OBJECT_ID('[Flood].[FloodApplicationFeedback]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationFeedback] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationFeedback];
	
	ALTER TABLE [Flood].[FloodApplicationFeedback] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationFeedback];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationFeedback]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationFeedback](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,
	[SectionId]								[smallint]						NOT NULL,
	[Feedback]								[varchar](4000)					NOT NULL,
	[RequestForCorrection]					[bit]							NOT NULL,
	[CorrectionStatus]						[varchar](100)					NOT NULL,
	[MarkRead]								[bit]							NOT NULL, 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodApplicationFeedback_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationFeedback] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationFeedback]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationFeedback] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationFeedback]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
