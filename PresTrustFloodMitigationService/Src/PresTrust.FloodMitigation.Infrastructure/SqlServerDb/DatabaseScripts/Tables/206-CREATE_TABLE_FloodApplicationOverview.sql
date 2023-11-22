 
IF OBJECT_ID('[Flood].[FloodApplicationOverview]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationOverview] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationOverview];
	
	ALTER TABLE [Flood].[FloodApplicationOverview] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationOverview];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationOverview]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationOverview](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[ApplicationId]							[integer]						NOT NULL,

	[NoOfHomes]								[integer]						NULL,
	[NoOfContiguousHomes]					[integer]						NULL,

	[NatlDisaster]							[bit]							DEFAULT 0,
	[NatlDisasterId]						[varchar](256)					NULL	,	 
	[NatlDisasterName]						[varchar](256)					NULL	,	 
	[NatlDisasterYear]						[smallint] 						NULL	,
	[NatlDisasterMonth]						[smallint] 						NULL	,

	[LOI]									[bit]							DEFAULT 0,
	[LOIStatus]								[varchar](50)					NULL	,
	[LOIApprovedDate]						[datetime]						NULL	,

	[FEMA_OR_NJDEP_Applied]					[bit]							DEFAULT 0,

	[FEMAApplied]							[bit]							DEFAULT 0,
	[FEMAStatus]							[varchar](50)					NULL	,
	[FEMAApprovedDate]						[datetime]						NULL	,
	[FEMADenialReason]						[varchar](512)					NULL	,

	[GreenAcresApplied]						[bit]							DEFAULT 0,
	[GreenAcresStatus]						[varchar](50)					NULL	,
	[GreenAcresApprovedDate]				[datetime]						NULL	,

	[BlueAcresApplied]						[bit]							DEFAULT 0,
	[BlueAcresStatus]						[varchar](50)					NULL	,
	[BlueAcresApprovedDate]					[datetime]						NULL	,

	[FundingAgenciesApplied]				[bit]							DEFAULT 0,
 
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[datetime]						NOT NULL,
	
	
CONSTRAINT [PK_FloodApplicationOverview_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationOverview] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationOverview]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO 

ALTER TABLE [Flood].[FloodApplicationOverview] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationOverview]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  
