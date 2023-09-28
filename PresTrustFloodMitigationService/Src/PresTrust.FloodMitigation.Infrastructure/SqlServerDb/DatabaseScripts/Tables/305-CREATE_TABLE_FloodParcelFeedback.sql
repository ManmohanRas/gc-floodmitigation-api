IF OBJECT_ID('[Flood].[FloodParcelFeedback]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelFeedback] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelFeedback];
	
	ALTER TABLE [Flood].[FloodParcelFeedback] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelFeedback];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelFeedback]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelFeedback](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,
	[PamsPin]								[varchar](76)					NOT NULL,
	[SectionId]								[smallint]						NOT NULL,
	[Feedback]								[varchar](4000)					NOT NULL,
	[RequestForCorrection]					[bit]							NOT NULL,
	[CorrectionStatus]						[varchar](100)					NOT NULL,
	[MarkRead]								[bit]							NOT NULL, 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodParcelFeedback_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodParcelFeedback] ADD CONSTRAINT [FK_ApplicationId_FloodParcelFeedback]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodParcelFeedback] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelFeedback]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
