IF OBJECT_ID('[Flood].[FloodAgencyFlap]') IS NOT NULL
BEGIN
	-- Drop Constraints
	
	ALTER TABLE [Flood].[FloodAgencyFlap] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodAgencyFlap];

END;
GO
  
-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodAgencyFlap]
GO

-- Create Table
CREATE TABLE [Flood].[FloodAgencyFlap](
	[Id]									[integer] 		IDENTITY(1,1)	NOT NULL,
	[AgencyId]							    [integer]						NOT NULL,
	[FlapApproved]							[bit]						    DEFAULT 0,
	[ApprovedDate]						    [datetime]					    NULL     ,
	[LastRevisedDate]						[datetime]					    NULL     ,
	[FlapMailToGrantee]						[datetime]					    NULL    ,
	[LastUpdatedBy]							[varchar](128)					NULL	,
	[LastUpdatedOn]							[datetime]						NOT NULL,

CONSTRAINT [PK_FloodAgencyFlap_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Create Constraints

ALTER TABLE [Flood].[FloodAgencyFlap] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodAgencyFlap]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO  



