namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class represents api's query input model and returns the response object
/// </summary>
public class GetApplicationDocumentsBySectionQuery: IRequest<IEnumerable<ApplicationDocumentTypeViewModel>>
{
    public int ApplicationId { get; set; }
    public string SectionName { get; set; }
}
