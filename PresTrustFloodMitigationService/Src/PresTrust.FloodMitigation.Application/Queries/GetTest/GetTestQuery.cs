namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTestQuery : IRequest<GetTestQueryViewModel>
{
    public int Id { get; set; }

}
