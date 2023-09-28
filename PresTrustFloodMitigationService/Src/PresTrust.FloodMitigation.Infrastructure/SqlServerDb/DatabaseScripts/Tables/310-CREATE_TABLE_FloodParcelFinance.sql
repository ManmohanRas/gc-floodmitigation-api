IF OBJECT_ID('[Flood].[FloodParcelFinance]') IS NOT NULL
BEGIN
	-- Drop Constraints
	ALTER TABLE [Flood].[FloodParcelFinance] DROP CONSTRAINT IF EXISTS  [FK_ApplicationId_FloodParcelFinance];

	ALTER TABLE [Flood].[FloodParcelFinance] DROP CONSTRAINT IF EXISTS  [DF_LastUpdatedOn_FloodParcelFinance];

END;
GO


-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodParcelFinance]
GO

-- Create Table
CREATE TABLE [Flood].[FloodParcelFinance](
[Id]	                                [integer]     IDENTITY(1,1)               NOT NULL,
[ApplicationId]                         [integer] 								  NOT NULL,
[PamsPin]					            [varchar](76)							  NOT NULL,
[EstimatePurchasePrice]                 [decimal](18,2)					          NOT NULL,
[AdditionalSoftCostEstimate]            [decimal](18,2)					          NOT NULL,
[AppraisedValue]                        [decimal](18,2)					          NULL    ,
[AMV]                                   [decimal](18,2)					          NULL    ,
[TotalFEMABenifits]                     [decimal](18,2)					          NULL    ,
[DOBAffidavitType]						[varchar](128)					          NULL    ,
[DOBAffidavitAmtAmt]						[decimal](18,2)					          NULL    ,
[HardCostFMPAmt]						[decimal](18,2)					          NULL    ,
[SoftCostFMPAmt]                        [decimal](18,2)					          NULL    ,
[AppraisersFee]                         [decimal](18,2)					          NULL    ,
[SurveyorsFee]                          [decimal](18,2)					          NULL    ,
[LastUpdatedBy]							[varchar](128)					          NULL	  ,
[LastUpdatedOn]							[dateTime]						          NULL	  ,
CONSTRAINT [PK_FloodParcelFinance_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)ON [PRIMARY]

GO

-- Create Constraints
ALTER TABLE [Flood].[FloodParcelFinance] ADD CONSTRAINT FK_ApplicationId_FloodParcelFinance  FOREIGN KEY (ApplicationId) REFERENCES [Flood].FloodApplication(Id);
GO

ALTER TABLE [Flood].[FloodParcelFinance] WITH NOCHECK ADD  CONSTRAINT [DF_LastUpdatedOn_FloodParcelFinance]  DEFAULT (GETDATE()) FOR [LastUpdatedOn]
GO