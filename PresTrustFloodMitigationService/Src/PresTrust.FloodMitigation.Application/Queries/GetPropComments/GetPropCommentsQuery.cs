namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropCommentsQuery : IRequest<IEnumerable<GetPropCommentsQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
