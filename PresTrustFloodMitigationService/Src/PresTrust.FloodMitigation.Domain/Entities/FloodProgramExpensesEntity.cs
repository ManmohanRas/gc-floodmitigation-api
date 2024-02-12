namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodProgramExpensesEntity
{
    public int Id { get; set; }

    public string ExpenseYear { get; set; }

    public decimal? ExpenseAmount { get; set; }

    public DateTime? ExpenseDate { get; set; }

    public int CategoryId { get; set; }

    public string Comment { get; set; }

    public string LastUpdatedBy { get; set; }

    public DateTime LastUpdatedOn { get; set; }

    public ProgramExpensesEnum Category
    {
        get
        {
            return (ProgramExpensesEnum)CategoryId;
        }
        set
        {
            this.CategoryId = (int)value;
        }
    }
}
