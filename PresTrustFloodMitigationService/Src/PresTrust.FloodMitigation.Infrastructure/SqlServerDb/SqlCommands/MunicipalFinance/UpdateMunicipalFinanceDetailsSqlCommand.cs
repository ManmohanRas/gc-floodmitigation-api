namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateMunicipalFinanceDetailsSqlCommand
{
    private readonly string _sqlCommand =
     @"UPDATE     [Flood].[FloodMunicipalFinance]
                    SET  TaxRate                        =   @p_TaxRate
                        ,AnticipatedHistCollection      =   @p_AnticipatedHistCollection
                        ,AnnualTaxLevy	                =   @p_AnnualTaxLevy
                        ,Reimbursements	                =   @p_Reimbursements
                        ,CashReceipts	                =   @p_CashReceipts
                        ,Interest	                    =   @p_Interest
                        ,OtherRevenues	                =   @p_OtherRevenues
                        ,Disbursements	                =   @p_Disbursements
                        ,DebtPayments	                =   @p_DebtPayments
                        ,OtherExpenses                  =   @p_OtherExpenses
                        ,OtherRevenuesExplained         =   @p_OtherRevenuesExplained
                        ,OtherExpensesExplained         =   @p_OtherExpensesExplained
                        ,LastUpdatedOn                  =   GETDATE()
            WHERE       Id = @p_Id;";

    public UpdateMunicipalFinanceDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
