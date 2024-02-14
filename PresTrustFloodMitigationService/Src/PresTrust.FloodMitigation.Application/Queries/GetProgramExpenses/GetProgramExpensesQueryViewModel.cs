namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryViewModel
{
    public int Id { get; set; }
    public string ExpenseYear { get; set; }
    public decimal? ExpenseAmount { get; set; }
    public DateTime? ExpenseDate { get; set; }
    public string Category { get; set; }
    public string Comment { get; set; }
}