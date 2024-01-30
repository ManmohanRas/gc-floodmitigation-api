namespace PresTrust.FloodMitigation.Application.Queries;

public class GetAnnualFundingDetailsQuery : IRequest<IEnumerable<GetAnnualFundingDetailsQueryViewModel>>
{
    public int Id { get; set; }
}
