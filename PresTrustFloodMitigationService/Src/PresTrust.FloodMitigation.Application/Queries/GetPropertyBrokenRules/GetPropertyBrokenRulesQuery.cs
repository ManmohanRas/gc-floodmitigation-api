namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class represents api's query input model and returns the response object
/// </summary>
public class GetPropertyBrokenRulesQuery: IRequest<IEnumerable<GetPropertyBrokenRulesQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public string? UserId { get; set;}

}
