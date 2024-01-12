IF OBJECT_ID('[Flood].[FloodMunicipalFinance]') IS NOT NULL

-- Drop Table
DROP TABLE IF EXISTS [Flood].[FloodMunicipalFinance]
GO

-- Create Table
CREATE TABLE [Flood].[FloodMunicipalFinance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AgencyId] [int] NOT NULL,
	[FiscalYear] [int] NOT NULL,
	[TaxRate] [decimal](10, 3) NULL,
	[AnticipatedHistCollection] [decimal](18, 2) NULL,
	[AnnualTaxLevy] [decimal](18, 2) NULL,
	[Reimbursements] [decimal](18, 2) NULL,
	[CashReceipts] [decimal](18, 2) NULL,
	[Interest] [decimal](18, 2) NULL,
	[OtherRevenues] [decimal](18, 2) NULL,
	[OtherRevenuesExplained] [varchar](256) NULL,
	[Disbursements] [decimal](18, 2) NULL,
	[DebtPayments] [decimal](18, 2) NULL,
	[OtherExpenses] [decimal](18, 2) NULL,
	[OtherExpensesExplained] [varchar](256) NULL,
	
CONSTRAINT [PK_FloodMunicipalFinance_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

