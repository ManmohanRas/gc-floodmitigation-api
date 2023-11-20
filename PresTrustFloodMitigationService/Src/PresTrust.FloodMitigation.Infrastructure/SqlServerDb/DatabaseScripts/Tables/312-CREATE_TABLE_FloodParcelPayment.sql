IF OBJECT_ID('[Flood].[FloodParcelPayment]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelPayment];

	ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_HardCostPaymentStatusId_FloodParcelPayment];

	ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [FK_SoftCostPaymentStatusId_FloodParcelPayment];

	ALTER TABLE [Flood].[FloodParcelPayment] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelPayment];

END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelPayment]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelPayment](
[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
[ApplicationId]                         [integer] 								  NOT NULL,
[PamsPin]					            [varchar](76)							  NOT NULL,
[HardCostPaymentTypeId]                 [integer]                                 NULL    ,
[HardCostPaymentDate]                   [datetime]                                NULL    ,
[HardCostPaymentStatusId]               [bit]                                     NOT NULL,
[SoftCostPaymentTypeId]                 [integer]                                 NULL    ,
[SoftCostPaymentDate]                   [datetime]                                NULL    ,
[SoftCostPaymentStatusId]               [bit]                                     NOT NULL,
[LastUpdatedBy]							[varchar](128)					          NULL	  ,
[LastUpdatedOn]							[datetime]						          NOT NULL,
CONSTRAINT [PK_FloodParcelPayment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodParcelPayment] ADD CONSTRAINT FK_ApplicationId_FloodParcelPayment  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodParcelPayment] WITH NOCHECK ADD  CONSTRAINT [DF_HardCostPaymentStatusId_FloodParcelPayment]  DEFAULT (0) FOR [HardCostPaymentStatusId]
GO

ALTER TABLE [Flood].[FloodParcelPayment] WITH NOCHECK ADD  CONSTRAINT [DF_SoftCostPaymentStatusId_FloodParcelPayment]  DEFAULT (0) FOR [SoftCostPaymentStatusId]
GO

ALTER TABLE [Flood].[FloodParcelPayment] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelPayment]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO