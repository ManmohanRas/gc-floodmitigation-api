namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationOverviewQuery : IRequest<GetApplicationOverviewQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
