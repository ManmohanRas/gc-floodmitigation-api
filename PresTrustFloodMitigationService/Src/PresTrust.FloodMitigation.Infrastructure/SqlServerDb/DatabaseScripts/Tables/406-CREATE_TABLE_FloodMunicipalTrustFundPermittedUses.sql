IF OBJECT_ID('[Flood].[FloodMunicipalTrustFundPermittedUses]') IS NOT NULL


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodMunicipalTrustFundPermittedUses]
GO

-- Create Table
CREATE TABLE [Flood].[FloodMunicipalTrustFundPermittedUses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AgencyId] [int] NOT NULL,
	[YearOfInception] [char](4) NULL,
	[AcquisitionOfLands] [bit] NULL,
	[AcquisitionOfFarmLands] [bit] NULL,
	[DevelopmentOfLands] [bit] NULL,
	[MaintenanceOfLands] [bit] NULL,
	[SalariesAndBenefits] [bit] NULL,
	[BondDownPayments] [bit] NULL,
	[HistoricPreservation] [bit] NULL,
	[OpenspaceMasterPlan] [bit] NULL,
	[OpenspaceMasterPlanDate] [datetime] NULL,
	[GreenAcresGrant] [bit] NULL,
	[TrustFundComments] [varchar](2000) NULL,
	[Other] [varchar](2000) NULL,
CONSTRAINT [PK_FloodMunicipalTrustFundPermittedUses_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

