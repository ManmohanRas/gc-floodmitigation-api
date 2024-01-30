IF OBJECT_ID('[Flood].[FloodLockedParcel]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodLockedParcel];

	ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodLockedParcel];

	ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [DF_IsValidPamsPin_FloodLockedParcel];

	ALTER TABLE [Flood].[FloodLockedParcel] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodLockedParcel];

END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodLockedParcel]
GO

-- Create Table
CREATE TABLE [Flood].[FloodLockedParcel](
	[Id]						[integer] 			IDENTITY(1,1)			NOT NULL,
	[ApplicationId]				[integer]									NOT NULL,
	[PamsPin]					[varchar](76)								NOT NULL,
	[AgencyID]					[varchar](8)								NULL,
	[Block]						[varchar](20)								NULL,
	[Lot]						[varchar](20)								NULL,
	[QualificationCode]			[varchar](22)								NULL,
	[Latitude]					[varchar](50)								NULL,
	[Longitude]					[varchar](50)								NULL,
	[StreetNo]					[varchar](50)								NULL,
	[StreetAddress]				[varchar](50)								NULL,
	[Acreage]					[decimal](18,4)								NULL,
	[OwnersName]				[varchar](70)								NULL,
	[OwnersAddress1]			[varchar](128)								NULL,
	[OwnersAddress2]			[varchar](128)								NULL,
	[OwnersCity]				[varchar](128)								NULL,
	[OwnersState]				[varchar](128)								NULL,
	[OwnersZipcode]				[varchar](20)								NULL,
	[SquareFootage]				[decimal](18,2)								NULL,
	[YearOfConstruction]		[smallint]									NULL,
	[TotalAssessedValue]		[decimal](18,2)								NULL,
	[LandValue]					[decimal](18,2)								NULL,
	[ImprovementValue]			[decimal](18,2)								NULL,
	[AnnualTaxes]				[decimal](18,2)								NULL,
	[IsValidPamsPin]			[bit]										NOT NULL,
	[TargetAreaId]				[integer]									NULL,
	[DateOfFLAP]				[datetime]									NULL,
	[IsElevated]				[bit]										NULL,
	[IsActive]					[bit]										NOT NULL,
	[LastUpdatedBy]				[varchar](128)								NULL,
	[LastUpdatedOn]				[datetime]									NOT NULL,
	
CONSTRAINT [PK_FloodLockedParcel_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Flood].[FloodLockedParcel] ADD CONSTRAINT [FK_ApplicationId_FloodLockedParcel]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodLockedParcel] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodLockedParcel]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

ALTER TABLE [Flood].[FloodLockedParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsValidPamsPin_FloodLockedParcel]  DEFAULT (0) FOR [IsValidPamsPin]
GO  

ALTER TABLE [Flood].[FloodLockedParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodLockedParcel]  DEFAULT (1) FOR [IsActive]
GO  
