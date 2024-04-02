IF OBJECT_ID('[rept].[FLOODAnnualSummaryReport]') IS NOT NULL
BEGIN
	-- Delete Constraint
	ALTER TABLE [rept].[FLOODAnnualSummaryReport] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedBy_ReptFLOODAnnualSummaryReport];

	ALTER TABLE [rept].[FLOODAnnualSummaryReport] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_ReptFLOODAnnualSummaryReport];

END;
Go

-- Drop Table
DROP TABLE IF EXISTS [rept].[FLOODAnnualSummaryReport]
GO

-- Create Table
CREATE TABLE [rept].[FLOODAnnualSummaryReport](
	[AgencyId]								[int]					NOT NULL,
	[FundingYear]							[int]					NOT NULL,
	[FundsEncumbered]		  			    [decimal](18, 2)		NULL,
	[FundsReimbursed]						[decimal](18, 2)		NULL,
	[PA_Submitted]						    [int]         	    	NULL,
	[PA_Active]					 	        [int]      				NULL,
	[PA_Rejected]							[int]					NULL,
	[PA_Withdrawn]							[int]					NULL,
	[PA_Closed]								[int]					NULL,
	[P_OfHomes]								[int]					NULL,
	[P_Pending]								[int]					NULL,
	[P_Preserved]							[int]					NULL,
	[P_Withdrawn]							[int]					NULL,
	[P_Rejected]							[int]					NULL,
	[P_PropertyExpired]						[int]					NULL,
	[P_PropertyGrantExpired]				[int]					NULL,
	[HardCosts]						        [decimal](18, 2)		NULL,
	[SoftCosts]						        [decimal](18, 2)		NULL,
	[CountyExpenses]						[decimal](18, 2)		NULL,
	[TotalAllocated]						[decimal](18, 2)		NULL,
	[FundsSpent]							[decimal](18, 2)		NULL,
	[ExpiringWithInAYear]					[int]					NULL,
	[LastUpdatedBy]							[varchar](128)			NULL,
	[LastUpdatedOn]							[datetime]				NULL
) ON [PRIMARY]
GO

ALTER TABLE [rept].[FLOODAnnualSummaryReport] ADD  CONSTRAINT [DF_LastUpdatedBy_ReptFLOODAnnualSummaryReport]  DEFAULT ('SQL Job') FOR [LastUpdatedBy]
GO

ALTER TABLE [rept].[FLOODAnnualSummaryReport] ADD  CONSTRAINT [DF_LastUpdatedOn_ReptFLOODAnnualSummaryReport]  DEFAULT (getdate()) FOR [LastUpdatedOn]
GO
