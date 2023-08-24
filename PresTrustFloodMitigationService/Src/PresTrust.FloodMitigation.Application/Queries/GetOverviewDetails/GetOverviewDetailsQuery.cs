namespace PresTrust.FloodMitigation.Application.Queries;

public class GetOverviewDetailsQuery : IRequest<GetOverviewDetailsQueryViewModel>
{
    public int ApplicationId { get; set; }
}
