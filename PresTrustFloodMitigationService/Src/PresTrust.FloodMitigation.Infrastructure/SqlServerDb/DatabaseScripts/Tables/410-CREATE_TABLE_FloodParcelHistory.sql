IF OBJECT_ID('[Flood].[FloodParcelHistory]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [FK_ParcelId_FloodParcelHistory];

	ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelHistory];

	ALTER TABLE [Flood].[FloodParcelHistory] DROP CONSTRAINT IF EXISTS  [DF_IsActive_FloodParcelHistory];

END
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelHistory]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelHistory](
	[Id]								[integer] 		IDENTITY(1,1)	NOT NULL,
	[ParcelId]							[integer] 						NOT NULL,
	[CurrentPamsPin]					[varchar](76)					NOT NULL,
	[PreviousPamsPin]					[varchar](76)					NOT NULL,
	[Section]							[varchar](128)					NULL,
	[Acres]								[decimal](18,4)					NULL,
	[AcresToBeAcquired]					[decimal](18,4)					NULL,
	[Partial]							[bit]							NULL,
	[InterestType]						[varchar](100)					NULL,
	[IsThisAnExclusionArea]				[bit]							NULL,
	[Notes]								[varchar](4000)					NULL,
	[EasementId]						[varchar](100)					NULL,
	[IsActive]							[bit]							NOT NULL,
	[ChangeType]                        [varchar](100)                  NULL,
	[ChangeDate]						[datetime]                      NULL,
	[ReasonForChange]					[varchar](4000)					NULL,
	[LastUpdatedBy]						[varchar](128)					NOT NULL,
	[LastUpdatedOn]						[datetime]						NOT NULL,
	
CONSTRAINT [PK_FloodParcelHistory_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints  

ALTER TABLE [Flood].[FloodParcelHistory] ADD CONSTRAINT [FK_ParcelId_FloodParcelHistory] FOREIGN KEY (ParcelId) REFERENCES [Flood].[FloodParcel](Id);
GO

ALTER TABLE [Flood].[FloodParcelHistory] WITH NOCHECK ADD  CONSTRAINT [DF_IsActive_FloodParcelHistory]  DEFAULT (0) FOR [IsActive]
GO 

ALTER TABLE [Flood].[FloodParcelHistory] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelHistory]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
