namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropReleaseOfFundsQuery: IRequest<GetPropReleaseOfFundsQueryViewModel>
{
    public int ApplicationId { get; set; }
    public int Pamspin { get; set; }
}
