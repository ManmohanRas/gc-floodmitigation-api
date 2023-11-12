namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationDocumentChecklistQuery : IRequest<IEnumerable<ApplicationDocumentChecklistSectionViewModel>>
{
    public int ApplicationId { get; set; }
}
