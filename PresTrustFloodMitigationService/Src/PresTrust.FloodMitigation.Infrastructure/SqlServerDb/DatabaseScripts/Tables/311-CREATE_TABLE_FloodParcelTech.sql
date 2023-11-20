IF OBJECT_ID('[Flood].[FloodParcelTech]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelTech] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelTech];
		

	ALTER TABLE [Flood].[FloodParcelTech] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelTech];

END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelTech]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelTech](
	[Id]												[integer] 			IDENTITY(1,1)			NOT NULL,
	[ApplicationId]										[integer]									NOT NULL,
	[PamsPin]											[varchar](76)								NOT NULL,
	[FEMASevereRepetitiveLossList]                      [bit]										NULL	,
	[FEMARepetitiveLossList]                            [bit]										NULL	,
	[IsthepropertywithinthePassaicRiverBasin]           [bit]										NULL	,
	[IsthepropertywithinFloodway]                       [bit]										NULL	,
	[IsthepropertywithinFloodplain]                     [bit]										NULL	,
	[Claim10Years]                                      [integer]									NULL	,
	[TotalOfClaims]                                     [decimal](18,2)								NULL	,
	[BenefitCostRatio]                                  [varchar](256)								NULL	,
	[FEMACommunityId]                                   [varchar](256)							    NULL	,
	[FirmEffectiveDate]                                 [datetime]									NULL	,
	[FirmPanel]                                         [varchar](256)								NULL	,
	[FirmPanelFinal]                                    [varchar](256)								NULL	,
	[FloodZoneDesignation]                              [varchar](256)								NULL	,
	[BaseFloodElevation]                                [varchar](256)								NULL	,
	[BaseFloodElevationFinal]                           [varchar](256)								NULL	,
	[RiverId]                                           [varchar](256)								NULL	,
	[RiverIdFinal]										[varchar](256)								NULL	,
	[FisEffectiveDate]                                  [datetime]									NULL	,
	[FloodProfile]                                      [varchar](256)								NULL	,
	[FloodProfileFinal]                                 [varchar](256)								NULL	,
	[FloodSource]                                       [varchar](256)								NULL	,
	[FirstFloodElevation]                               [varchar](256)								NULL	,
	[FirstFloodElevationFinal]                          [varchar](256)								NULL	,
	[StreambedElevation]                                [varchar](256)								NULL	,
	[StreambedElevationFinal]                           [varchar](256)								NULL	,
	[ElevationBeforeMitigation]                         [varchar](256)								NULL	,
	[ElevationBeforeMitigationFinal]                    [varchar](256)								NULL	,
	[FloodType]                                         [varchar](256)								NULL	,
	[TenPercent]                                        [integer]									NULL	,
	[TwoPercent]                                        [integer]									NULL	,
	[OnePercent]                                        [integer]									NULL	,
	[PointOnePercent]									[integer]									NULL	,
	[LastUpdatedBy]										[varchar](128)								NULL	,
	[LastUpdatedOn]										[datetime]									NOT NULL,
	
CONSTRAINT [PK_FloodParcelTech_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodParcelTech] ADD CONSTRAINT [FK_ApplicationId_FloodParcelTech]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO

ALTER TABLE [Flood].[FloodParcelTech] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelTech]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
