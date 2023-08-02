IF OBJECT_ID('[Flood].[FloodParcelAudit]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelAudit] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelAudit];

	ALTER TABLE [Flood].[FloodParcelAudit] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodParcelAudit];

END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelAudit]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelAudit](
	[Id]						[integer] 								NOT NULL,
	[PamsPin]					[nvarchar](76)							NOT NULL,
	[AgencyID]					[nvarchar](8)							NULL,
	[Block]						[nvarchar](20)							NULL,
	[Lot]						[nvarchar](20)							NULL,
	[QualificationCode]			[nvarchar](22)							NULL,
	[StreetNo]					[nvarchar](50)							NULL,
	[StreetAddress]				[nvarchar](50)							NULL,
	[Acreage]					[decimal](5,4)							NULL,
	[OwnersName]				[nvarchar](70)							NULL,
	[OwnersAddress1]			[nvarchar](128)							NULL,
	[OwnersAddress2]			[nvarchar](128)							NULL,
	[SquareFootage]				[integer]								NULL,
	[YearOfConstruction]		[smallint]								NULL,
	[TotalAssessedValue]		[integer]								NULL,
	[LandValue]					[integer]								NULL,
	[ImprovementValue]			[integer]								NULL,
	[AnnualTaxes]				[decimal](9,2)							NULL,
	[IsFLAP]					[bit]									NULL,
	[DateOfFLAP]				[DateTime]								NULL,
	[StatusId]					[smallint]								NOT NULL,
	[LastUpdatedBy]				[varchar](128)							NULL,
	[LastUpdatedOn]				[DateTime]								NOT NULL,
	[IsActive]					[bit]									NOT NULL,
	
CONSTRAINT [PK_FloodParcelAudit_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodParcelAudit] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelAudit]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

ALTER TABLE [Flood].[FloodParcelAudit] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodParcelAudit]  DEFAULT (1) FOR [IsActive]
GO  
