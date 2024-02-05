namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryViewModel
{
    public int ExpenseId { get; set; }

    public string ExpenseYear { get; set; }

    public decimal ExpenseAmount { get; set; }

    public string ExpenseDate { get; set; }

    public string Category { get; set; }

    public string Comment { get; set; }

}
