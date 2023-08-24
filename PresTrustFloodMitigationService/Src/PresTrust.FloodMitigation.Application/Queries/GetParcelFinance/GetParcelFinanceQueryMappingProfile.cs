namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelFinanceQueryMappingProfile: Profile
{
    public GetParcelFinanceQueryMappingProfile()
    {
        CreateMap<FloodParcelFinanceEntity, GetParcelFinanceQueryViewModel>();
    }
}
