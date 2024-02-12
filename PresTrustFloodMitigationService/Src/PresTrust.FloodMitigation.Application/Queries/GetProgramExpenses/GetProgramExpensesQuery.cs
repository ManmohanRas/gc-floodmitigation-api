namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQuery : IRequest<IEnumerable<GetProgramExpensesQueryViewModel>>
{
    public int Id { get; set; }
}
