IF OBJECT_ID('[Flood].[FloodParcelTracking]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodParcelTracking] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodParcelTracking];
 
	ALTER TABLE [Flood].[FloodParcelTracking] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodParcelTracking];
END;
GO
 
DROP TABLE IF EXISTS [Flood].[FloodParcelTracking] 
GO
 
CREATE TABLE [Flood].[FloodParcelTracking](
	[Id]					[integer]			IDENTITY(1,1)	NOT NULL,
	[ApplicationId]			[integer]							NOT NULL,
	[PamsPin]				[varchar](76)						NOT NULL,
	[ClosingDate]			[datetime]							NULL,
	[DeedBook]				[varchar](128)						NULL,
	[DeedPage]				[varchar](128)						NULL,
	[DeedDate]				[datetime]							NULL,
	[DemolitionDate]		[datetime]							NULL,
	[SiteVisitConfirmDate]	[datetime]							NULL,
	[PublicPark]			[Bit]								NULL,
	[RainGarden]			[Bit]								NULL,
	[CommunityGarden]		[Bit]								NULL,
	[ActiveRecreation]		[Bit]								NULL,
	[NaturalHabitat]		[Bit]								NULL,
	[LastUpdatedBy]			[varchar](128)						NULL,
	[LastUpdatedOn]			[datetime]							NOT NULL
 
CONSTRAINT [PK_FloodParcelTracking_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
 
ALTER TABLE [Flood].[FloodParcelTracking] ADD CONSTRAINT [FK_ApplicationId_FloodParcelTracking]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO
 
ALTER TABLE [Flood].[FloodParcelTracking] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelTracking]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO