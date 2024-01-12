namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public class CreateMunicipalFinanceDetailsSqlCommand
    {
	private readonly string _sqlCommand =
                 @"INSERT INTO [Flood].[FloodMunicipalFinance]
						(
							   [AgencyId]
							  ,[FiscalYear]
							  ,[TaxRate]
							  ,[AnticipatedHistCollection]
							  ,[AnnualTaxLevy]
							  ,[Reimbursements]
							  ,[CashReceipts]
							  ,[Interest]
							  ,[OtherRevenues]
							  ,[OtherRevenuesExplained]
							  ,[Disbursements]
							  ,[DebtPayments]
							  ,[OtherExpenses]
							  ,[OtherExpensesExplained]
						)

						VALUES
						(
							 @p_AgencyId
							,@p_FiscalYear
							,@p_TaxRate
							,@p_AnticipatedHistCollection
							,@p_AnnualTaxLevy
							,@p_Reimbursements							
							,@p_CashReceipts
							,@p_Interest
							,@p_OtherRevenues
							,@p_OtherRevenuesExplained
							,@p_Disbursements
							,@p_DebtPayments
							,@p_OtherExpenses
							,@p_OtherExpensesExplained
						);

				 SELECT CAST( SCOPE_IDENTITY() AS INT);";


	public CreateMunicipalFinanceDetailsSqlCommand()
	{
	}

	public override string ToString()
	{
		return _sqlCommand;
	}
}
