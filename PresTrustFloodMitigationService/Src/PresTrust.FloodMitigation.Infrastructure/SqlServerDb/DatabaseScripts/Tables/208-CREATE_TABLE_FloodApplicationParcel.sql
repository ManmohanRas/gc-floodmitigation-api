IF OBJECT_ID('[Flood].[FloodApplicationParcel]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationParcel] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationParcel];

	ALTER TABLE [Flood].[FloodApplicationParcel] DROP CONSTRAINT IF EXISTS  [DF_IsLocked_FloodApplicationParcel];
END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationParcel]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationParcel](
	[ApplicationId]					[integer]						NOT NULL,
	[PamsPin]						[varchar](76)					NOT NULL,
	[StatusId]						[smallint]						NOT NULL,
	[IsLocked]						[bit]							NOT NULL,
	[IsSubmitted]                   [bit]                           DEFAULT 0,
	[IsApproved]                    [bit]                           DEFAULT 0,
	[WaitingApproved]               [bit]                           DEFAULT 0,
	[RejectedApproved]              [bit]                           DEFAULT 0

	)
GO

-- Create Constraint
ALTER TABLE [Flood].[FloodApplicationParcel] ADD CONSTRAINT [FK_ApplicationId_FloodApplicationParcel]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO 

ALTER TABLE [Flood].[FloodApplicationParcel] WITH NOCHECK ADD  CONSTRAINT [DF_IsLocked_FloodApplicationParcel]  DEFAULT (0) FOR [IsLocked]
GO
