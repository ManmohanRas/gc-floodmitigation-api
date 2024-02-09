namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramExpensesCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string ExpenseYear { get; set; }
    public decimal ExpenseAmount { get; set; }
    public string ExpenseDate { get; set; }
    public string Category { get; set; }
    public string Comment { get; set; }
}

