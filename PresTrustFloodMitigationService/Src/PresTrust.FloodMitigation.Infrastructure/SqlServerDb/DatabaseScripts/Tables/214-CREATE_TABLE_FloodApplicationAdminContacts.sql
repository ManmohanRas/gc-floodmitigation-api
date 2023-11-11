IF OBJECT_ID('[Flood].[FloodContacts]') IS NOT NULL
BEGIN

	ALTER TABLE [Flood].[FloodContacts] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodContacts];

	ALTER TABLE [Flood].[FloodContacts] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodContacts];

	
END;
GO

DROP TABLE IF EXISTS [Flood].[FloodContacts]
GO

CREATE TABLE [Flood].[FloodContacts](
	[Id]					[integer]			IDENTITY(1,1)	NOT NULL,
	[ApplicationId]			[integer]							NOT NULL,
	[ContactName]			[varchar](76)						NOT NULL,
	[Agency]				[varchar](128)						NOT NULL,
	[Email]					[varchar](128)						NOT NULL,
	[MainNumber]			[varchar](128)						NOT NULL,
	[AlternateNumber]		[varchar](128)						NOT NULL,
	[SelectContact]			[bit]								NOT NULL,
	[LastUpdatedBy]			[varchar](128)						NULL	,
	[LastUpdatedOn]			[DateTime]							NOT NULL,
CONSTRAINT [PK_FloodContacts_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodContacts] ADD CONSTRAINT [FK_ApplicationId_FloodContacts]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO

ALTER TABLE [Flood].[FloodContacts] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodContacts]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
