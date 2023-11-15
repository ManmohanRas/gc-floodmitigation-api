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
	[BenefitCostRatio]                                  [decimal](18,2)								NULL	,
	[FEMACommunityId]                                   [varchar](128)							    NULL	,
	[FirmEffectiveDate]                                 [DateTime]									NULL	,
	[FirmPanel]                                         [varchar](256)								NULL	,
	[FirmPanelFinal]                                    [varchar](256)								NULL	,
	[FloodZoneDesignation]                              [varchar](256)								NULL	,
	[BaseFloodElevation]                                [integer]								    NULL	,
	[BaseFloodElevationFinal]                           [integer]									NULL	,
	[RiverId]                                           [integer]									NULL	,
	[RiverIdFinal]										[integer]									NULL	,
	[FisEffectiveDate]                                  [DateTime]									NULL	,
	[FloodProfile]                                      [varchar](256)								NULL	,
	[FloodProfileFinal]                                 [varchar](256)								NULL	,
	[FloodSource]                                       [varchar](256)								NULL	,
	[FirstFloodElevation]                               [decimal](18,2)								NULL	,
	[FirstFloodElevationFinal]                          [decimal](18,2)								NULL	,
	[StreambedElevation]                                [decimal](18,2)								NULL	,
	[StreambedElevationFinal]                           [decimal](18,2)								NULL	,
	[ElevationBeforeMitigation]                         [decimal](18,2)								NULL	,
	[ElevationBeforeMitigationFinal]                    [decimal](18,2)								NULL	,
	[FloodType]                                         [varchar](256)								NULL	,
	[TenPercent]                                        [integer]									NULL	,
	[TwoPercent]                                        [integer]									NULL	,
	[OnePercent]                                        [integer]									NULL	,
	[PointOnePercent]									[integer]									NULL	,
	[LastUpdatedBy]										[varchar](128)								NULL	,
	[LastUpdatedOn]										[DateTime]									NOT NULL,
	
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
