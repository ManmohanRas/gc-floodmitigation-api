namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelFinanceQuery: IRequest<GetParcelFinanceQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
}
