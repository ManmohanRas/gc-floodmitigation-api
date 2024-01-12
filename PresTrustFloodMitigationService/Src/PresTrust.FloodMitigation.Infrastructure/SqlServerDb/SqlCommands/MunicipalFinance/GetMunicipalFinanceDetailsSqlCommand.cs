namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public class GetMunicipalFinanceDetailsSqlCommand
    {
	private readonly string _sqlCommand =
           @"SELECT	   [Id]
		                  ,[AgencyId]
		                  ,[FiscalYear]
		                  ,ISNULL([TaxRate], 0)						AS	TaxRate
		                  ,ISNULL([AnticipatedHistCollection], 0)	AS	AnticipatedHistCollection		
		                  ,ISNULL([AnnualTaxLevy], 0)				AS	AnnualTaxLevy		
		                  ,ISNULL([Reimbursements], 0)				AS	Reimbursements		
		                  ,ISNULL([CashReceipts], 0)				AS	CashReceipts		
		                  ,ISNULL([Interest], 0)					AS	Interest		
		                  ,ISNULL([OtherRevenues], 0)				AS	OtherRevenues		
		                  ,ISNULL([OtherRevenuesExplained], '')		AS	OtherRevenuesExplained		
		                  ,ISNULL([Disbursements], 0)				AS	Disbursements		
		                  ,ISNULL([DebtPayments], 0)				AS	DebtPayments		
		                  ,ISNULL([OtherExpenses], 0)				AS	OtherExpenses		
		                  ,ISNULL([OtherExpensesExplained], '')		AS	OtherExpensesExplained		
                 FROM [Flood].[FloodMunicipalFinance]
                 WHERE	AgencyId = @p_AgencyId 
                 AND ((@p_UseYearFilter = 1 AND FiscalYear >= 2005) OR (@p_UseYearFilter = 0 AND 1 = 1))
                 ORDER BY FiscalYear DESC;";
	public GetMunicipalFinanceDetailsSqlCommand() { }

	public override string ToString()
	{
		return _sqlCommand;
	}
}
