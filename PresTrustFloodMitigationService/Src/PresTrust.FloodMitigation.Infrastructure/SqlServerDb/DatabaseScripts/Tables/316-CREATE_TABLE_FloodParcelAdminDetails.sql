IF OBJECT_ID('[Flood].[FloodParcelAdminDetails]') IS NOT NULL
BEGIN
	ALTER TABLE [Flood].[FloodParcelAdminDetails] DROP CONSTRAINT IF EXISTS [FK_ApplicationId_FloodParcelAdminDetails];

	ALTER TABLE [Flood].[FloodParcelAdminDetails] DROP CONSTRAINT IF EXISTS [DF_LastUpdatedOn_FloodParcelAdminDetails];
END;
GO

DROP TABLE IF EXISTS [Flood].[FloodParcelAdminDetails]
GO

CREATE TABLE [Flood].[FloodParcelAdminDetails](
	[Id]									[integer]				IDENTITY(1,1)				NOT NULL,
	[ApplicationId]							[integer]											NOT NULL,
	[PamsPin]								[varchar](76)										NOT NULL,
	[DOBDocumentsMissingDate]               [datetime]											NULL,
	[FMCFinalApprovalDate]                  [datetime]											NULL,
	[FMCFinalNumber]                        [varchar](128)										NULL,
	[BCCFinalApprovalDate]                  [datetime]											NULL,
	[BCCFinalNumber]                        [varchar](128)										NULL,
	[MunicipalPurchaseDate]                 [datetime]											NULL,
	[MunicipalPurchaseNumber]               [varchar](128)										NULL,
	[GrantAgreementDate]                    [datetime]											NULL,
	[GrantAgreementExpirationDate]          [datetime]											NULL,
	[DueDiligenceDocumentsMissingDate]      [datetime]											NULL,
	[ScheduleClosingDate]                   [datetime]											NULL,
	[SoftCostReimbursementRequestDate]      [datetime]											NULL,
	[FMCSoftCostReimbApprovalDate]          [datetime]											NULL,
	[FMCSoftCostReimbApprovalNumber]        [varchar](128)										NULL,
	[BCCSoftCostReimbApprovalDate]          [datetime]											NULL,
	[BCCSoftCostReimbApprovalNumber]        [varchar](128)										NULL,
	[DoesHomeOwnerHaveNFIPInsurance]        [bit]												NULL,
	[IsDEPInvolved]                         [bit]												NULL,
	[IsPARRequestedbyFunder]                [bit]												NULL,
	[LastUpdatedBy]							[varchar](128)										NULL,
	[LastUpdatedOn]							[datetime]											NOT NULL,
CONSTRAINT [PK_FloodParcelAdminDetails_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodParcelAdminDetails] ADD CONSTRAINT [FK_ApplicationId_FloodParcelAdminDetails]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO

ALTER TABLE [Flood].[FloodParcelAdminDetails] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelAdminDetails]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
