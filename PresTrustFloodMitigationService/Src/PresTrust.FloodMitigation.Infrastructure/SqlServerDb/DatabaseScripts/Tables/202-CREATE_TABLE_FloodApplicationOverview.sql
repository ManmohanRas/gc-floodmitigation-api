 
IF OBJECT_ID('[Flood].[FloodOverview]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodOverview] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodOverview];
	
	ALTER TABLE [Flood].[FloodOverview] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodOverview];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodOverview]
GO

-- Create Table
CREATE TABLE [Flood].[FloodOverview](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,

	[FactorHomes]							[integer]						NOT NULL,
	[FactorContiguousHomes]					[integer]						NOT NULL,

	[NatlDisaster]							[bit]							DEFAULT 0,
	[NatlDisasterId]						[int]							NULL	,	 
	[NatlDisasterName]						[varchar](256)					NULL	,	 
	[NatlDisasterYear]						[smallint] 						NULL	,
	[NatlDisasterMonth]						[smallint] 						NULL	,

	[LOI]									[bit]							DEFAULT 0,
	[LOIStatus]								[varchar](50)					NULL	,
	[LOIApprovedDate]						[date]							NULL	,

	[FEMA_OR_NJDEP_Applied]					[bit]							DEFAULT 0,

	[FEMAApplied]							[bit]							DEFAULT 0,
	[FEMAStatus]							[varchar](50)					NULL	,
	[FEMAApprovedDate]						[date]							NULL	,
	[FEMADenialReason]						[varchar](max)					NULL	,

	[GreenAcresApplied]						[bit]							DEFAULT 0,
	[GreenAcresStatus]						[varchar](50)					NULL	,
	[GreenAcresApprovedDate]				[date]							NULL	,

	[BlueAcresApplied]						[bit]							DEFAULT 0,
	[BlueAcresStatus]						[varchar](50)					NULL	,
	[BlueAcresApprovedDate]					[date]							NULL	,

	[FundingAgenciesApplied]			[bit]							DEFAULT 0,
 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[DateTime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodOverview_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodOverview] ADD CONSTRAINT [FK_ApplicationId_FloodOverview]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodOverview] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodOverview]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  

  

 