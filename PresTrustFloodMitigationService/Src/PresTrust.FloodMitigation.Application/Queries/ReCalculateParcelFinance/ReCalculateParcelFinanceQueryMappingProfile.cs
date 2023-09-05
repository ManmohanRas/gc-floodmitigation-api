namespace PresTrust.FloodMitigation.Application.Queries;

public class ReCalculateParcelFinanceQueryMappingProfile: Profile
{
    public ReCalculateParcelFinanceQueryMappingProfile()
    {
        CreateMap<ReCalculateParcelFinanceQuery, ReCalculateParcelFinanceQueryViewModel>();
    }
}
