-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationLegacy]

-- Create Table
CREATE TABLE [Flood].[FloodApplicationLegacy](
	[LegacyApplicationId]			[integer]						NOT NULL,
	[LegacyApplicationType]			[varchar](256)					NOT NULL,
	[LegacyApplicationSubType]		[varchar](256)					NOT NULL,
	[LegacyApplicationStatus]		[varchar](256)					NOT NULL,
	[LegacyAgencyId]				[integer]						NOT NULL,
	[FloodApplicationId]			[integer]						NULL,
CONSTRAINT [PK_FloodApplicationLegacy_Id] PRIMARY KEY CLUSTERED 
(
	[LegacyApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
