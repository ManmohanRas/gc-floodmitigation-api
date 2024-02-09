namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryViewModel
{
    public int Id { get; set; }
    public string ExpenseYear { get; set; }
    public decimal? ExpenseAmount { get; set; }
    public DateTime? ExpenseDate { get; set; }
    public int? CategoryId { get; set; }
    public string Comment { get; set; }
}