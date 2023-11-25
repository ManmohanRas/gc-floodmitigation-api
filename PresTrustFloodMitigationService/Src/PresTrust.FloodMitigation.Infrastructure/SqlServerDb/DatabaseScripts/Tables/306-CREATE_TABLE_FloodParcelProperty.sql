IF OBJECT_ID('[Flood].[FloodParcelProperty]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelProperty] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelProperty];

END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelProperty]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelProperty](
	[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
	[ApplicationId]				[integer]								NOT NULL,
	[PamsPin]					[varchar](76)							NOT NULL,
	[Priority]			        [integer]								NOT NULL,
	[ValueEstimate]				[decimal](18,2)				            NULL,
	[EstimatedPurchasePrice]	[decimal](18,2)				            NULL,
	[BRV]						[decimal](18,2)							NULL,
	[NfipPolicyNo]				[varchar](128)							NULL,
	[SourceOfValueEstimate]		[varchar](128)							NULL,
	[FirstFloorElevation]		[decimal](18,2)							NULL,
	[StructureType]			    [integer]								NULL,
	[FoundationType]			[integer]								NULL,
	[OccupancyClass]			[integer]								NULL,
	[PercentageOfDamage]		[integer]								NULL,
	[HasContaminants]			[bit]									NULL,
	[IsLowIncomeHousing]		[bit]									NULL,
	[HasHistoricSignificance]	[bit]									NULL,
	[IsRentalProperty]			[bit]									NULL,
	[RentPerMonth]				[decimal](18,2)							NULL,
	[NeedSoftCost]				[bit]									NULL,
	[IsPreIrenePropertyOwner]	[bit]									NULL,
	[LastUpdatedBy]				[varchar](128)							NULL,
	[LastUpdatedOn]				[datetime]								NOT NULL
	
CONSTRAINT [PK_FloodParcelProperty_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodParcelProperty] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelProperty]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
