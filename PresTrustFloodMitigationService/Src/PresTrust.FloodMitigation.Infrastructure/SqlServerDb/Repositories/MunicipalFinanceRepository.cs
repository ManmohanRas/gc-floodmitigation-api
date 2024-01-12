namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class MunicipalFinanceRepository: IMunicipalFinanceRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public MunicipalFinanceRepository(
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodMunicipalFinanceEntity>> GetMunicipalFinanceDetails(int agencyId, bool useYearFilter = false)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new GetMunicipalFinanceDetailsSqlCommand();
        return (await conn.QueryAsync<FloodMunicipalFinanceEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_AgencyId = agencyId,
                                @p_UseYearFilter = useYearFilter
                            })).ToList();
    }

    public async Task<int> SaveMunicipalFinanceDetailsAsync(FloodMunicipalFinanceEntity municipalFinance)
    {
        if (municipalFinance == null) throw new ArgumentNullException();

        if (municipalFinance.Id > 0)
            return await UpdateMunicipalFinanceAsync(municipalFinance);
        else
            return await CreateMunicipalFinanceAsync(municipalFinance);
    }

    private async Task<int> CreateMunicipalFinanceAsync(FloodMunicipalFinanceEntity municipalFinance)
    {
        int result = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateMunicipalFinanceDetailsSqlCommand();
        result = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_AgencyId = municipalFinance.AgencyId,
                @p_FiscalYear = municipalFinance.FiscalYear,
                @p_TaxRate = municipalFinance.TaxRate,
                @p_AnticipatedHistCollection = municipalFinance.AnticipatedHistCollection,
                @p_AnnualTaxLevy = municipalFinance.AnnualTaxLevy,
                @p_Reimbursements = municipalFinance.Reimbursements,
                @p_CashReceipts = municipalFinance.CashReceipts,
                @p_Interest = municipalFinance.Interest,
                @p_OtherRevenues = municipalFinance.OtherRevenues,
                @p_OtherRevenuesExplained = municipalFinance.OtherRevenuesExplained,
                @p_Disbursements = municipalFinance.Disbursements,
                @p_DebtPayments = municipalFinance.DebtPayments,
                @p_OtherExpenses = municipalFinance.OtherExpenses,
                @p_OtherExpensesExplained = municipalFinance.OtherExpensesExplained
            });

        return result;
    }

    private async Task<int> UpdateMunicipalFinanceAsync(FloodMunicipalFinanceEntity municipalFinance)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateMunicipalFinanceDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = municipalFinance.Id,
                @p_FiscalYear = municipalFinance.FiscalYear,
                @p_TaxRate = municipalFinance.TaxRate,
                @p_AnticipatedHistCollection = municipalFinance.AnticipatedHistCollection,
                @p_AnnualTaxLevy = municipalFinance.AnnualTaxLevy,
                @p_Reimbursements = municipalFinance.Reimbursements,
                @p_Interest = municipalFinance.Interest,
                @p_CashReceipts = municipalFinance.CashReceipts,
                @p_OtherRevenues = municipalFinance.OtherRevenues,
                @p_OtherExpenses = municipalFinance.OtherExpenses,
                @p_Disbursements = municipalFinance.Disbursements,
                @p_DebtPayments = municipalFinance.DebtPayments,
                @p_OtherRevenuesExplained = municipalFinance.OtherRevenuesExplained,
                @p_OtherExpensesExplained = municipalFinance.OtherExpensesExplained
            });

        return municipalFinance.Id;
    }


}
