IF OBJECT_ID('[Flood].[FloodApplicationAdminDetails]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodApplicationAdminDetails] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodApplicationAdminDetails];

	ALTER TABLE [Flood].[FloodApplicationAdminDetails] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodApplicationAdminDetails];

END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodApplicationAdminDetails]
GO

-- Create Table
CREATE TABLE [Flood].[FloodApplicationAdminDetails](
[Id]	                                [integer]     IDENTITY(1,1)             NOT NULL,
[ApplicationId]                         [integer] 								NOT NULL,
[MunicipalResolutionDate]				[DateTime]								NULL,
[MunicipalResolutionNumber]             [varchar](128)  						NULL,
[FMCPreliminaryApprovalDate]			[DateTime]								NULL,
[FMCPreliminaryNumber]                  [varchar](128)							NULL,
[BCCPreliminaryApprovalDate]            [DateTime]								NULL,
[BCCPreliminaryNumber]                  [varchar](128)							NULL,
[ProjectDescription]                    [varchar](512)							NULL,
[FundingExpirationDate]                 [DateTime]								NULL,
[FirstFundingExpirationDate]            [DateTime]								NULL,
[SecondFundingExpirationDate]           [DateTime]								NULL,
[CommissionerMeetingDate]               [DateTime]								NULL,
[FirstCommitteeMeetingDate]             [DateTime]								NULL,
[SecondCommitteeMeetingDate]            [DateTime]								NULL,
[LastUpdatedBy]							[varchar](128)					        NULL,
[LastUpdatedOn]							[DateTime]						        NOT NULL,
CONSTRAINT [PK_FloodApplicationAdminDetails_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodApplicationAdminDetails] ADD CONSTRAINT FK_ApplicationId_FloodApplicationAdminDetails  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodApplicationAdminDetails] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodApplicationAdminDetails]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
