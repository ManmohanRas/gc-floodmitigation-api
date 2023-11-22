IF OBJECT_ID('[Flood].[FloodParcelSoftCost]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelSoftCost];

	ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [FK_SoftCostTypeId_FloodParcelSoftCost];

	ALTER TABLE [Flood].[FloodParcelSoftCost] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelSoftCost];

END;
GO

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelSoftCost]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelSoftCost](
	[Id]						[integer] 			IDENTITY(1,1)		NOT NULL,
	[ApplicationId]				[integer]								NOT NULL,
	[PamsPin]					[varchar](76)							NOT NULL,
	[SoftCostTypeId]			[integer]								NOT NULL,
	[VendorName]				[varchar](256)							NOT NULL,
	[InvoiceAmount]				[decimal](18,2)							NOT NULL,
	[PaymentAmount]				[decimal](18,2)							NOT NULL,
	[IsSubmitted]			    [bit]								    DEFAULT 0,
	[IsApproved]			    [bit]								    DEFAULT 0,
	[LastUpdatedBy]				[varchar](128)							NULL	,
	[LastUpdatedOn]				[datetime]								NOT NULL,
	
CONSTRAINT [PK_FloodParcelSoftCost_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Flood].[FloodParcelSoftCost] ADD CONSTRAINT [FK_ApplicationId_FloodParcelSoftCost]  FOREIGN KEY (ApplicationId) REFERENCES [Flood].[FloodApplication](Id);
GO

ALTER TABLE [Flood].[FloodParcelSoftCost] ADD CONSTRAINT [FK_SoftCostTypeId_FloodParcelSoftCost]  FOREIGN KEY (SoftCostTypeId) REFERENCES [Flood].[FloodParcelSoftCostType](Id);
GO

ALTER TABLE [Flood].[FloodParcelSoftCost] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelSoftCost]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO
