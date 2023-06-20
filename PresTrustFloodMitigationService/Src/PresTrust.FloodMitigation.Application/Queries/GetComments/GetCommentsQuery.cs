namespace PresTrust.FloodMitigation.Application.Queries;

public class GetCommentsQuery : IRequest<IEnumerable<GetCommentsQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public bool IsConsultantComment { get; set; }
}
