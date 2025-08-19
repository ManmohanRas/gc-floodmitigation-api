namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalFinanceCommand: IRequest<int>
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int AgencyId { get; set; }
    public int FiscalYear { get; set; }
    public decimal TaxRate { get; set; }
    public decimal AnticipatedHistCollection { get; set; }
    public decimal AnnualTaxLevy { get; set; }
    public decimal Reimbursements { get; set; }
    public decimal CashReceipts { get; set; }
    public decimal Interest { get; set; }
    public decimal OtherRevenues { get; set; }
    public string? OtherRevenuesExplained { get; set; }
    public decimal Disbursements { get; set; }
    public decimal DebtPayments { get; set; }
    public decimal OtherExpenses { get; set; }
    public string? OtherExpensesExplained { get; set; }
}
