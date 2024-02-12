namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteProgramExpensesCommand : IRequest<bool>
{
    public int Id { get; set; }
  
}
