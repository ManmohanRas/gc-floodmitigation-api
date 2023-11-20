IF OBJECT_ID('[Flood].[FloodParcelSurvey]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodParcelSurvey] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodParcelSurvey];

	ALTER TABLE [Flood].[FloodParcelSurvey] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodParcelSurvey];
END;
GO

DROP TABLE IF EXISTS [Flood].[FloodParcelSurvey]
GO

CREATE TABLE [Flood].[FloodParcelSurvey](
	[Id]					[integer]			IDENTITY(1,1)	NOT NULL,
	[ApplicationId]			[integer]							NOT NULL,
	[PamsPin]				[varchar](76)						NOT NULL,
	[Surveyor]				[varchar](256)						NULL,
	[SurveyDate]			[datetime]							NULL,
	[LastRevision]			[datetime]							NULL,
	[DateCorrected]			[datetime]							NULL,
	[LastUpdatedBy]			[varchar](128)						NULL,
	[LastUpdatedOn]			[datetime]							NOT NULL

CONSTRAINT [PK_FloodParcelSurvey_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodParcelSurvey] ADD CONSTRAINT [FK_ApplicationId_FloodParcelSurvey]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO

ALTER TABLE [Flood].[FloodParcelSurvey] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelSurvey]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
