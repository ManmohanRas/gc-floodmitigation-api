namespace PresTrust.FloodMitigation.Application.Queries;

public class GetAnnualFundingDetailsQueryMappingProfile : Profile
{
    public GetAnnualFundingDetailsQueryMappingProfile()
    {
        CreateMap<FloodAnnualFundingEntity, GetAnnualFundingDetailsQueryViewModel>();
    }
}
