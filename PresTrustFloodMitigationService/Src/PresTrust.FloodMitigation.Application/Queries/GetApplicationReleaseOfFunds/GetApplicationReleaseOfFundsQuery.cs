namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationReleaseOfFundsQuery: IRequest<GetApplicationReleaseOfFundsQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
