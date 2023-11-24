namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyCommentsQuery : IRequest<IEnumerable<GetPropertyCommentsQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
