namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelSurveyQuery : IRequest<GetParcelSurveyQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }

}
