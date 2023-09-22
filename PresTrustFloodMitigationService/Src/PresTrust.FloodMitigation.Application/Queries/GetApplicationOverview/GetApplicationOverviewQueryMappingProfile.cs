namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationOverviewQueryMappingProfile : Profile
{
    public GetApplicationOverviewQueryMappingProfile() 
    {
        CreateMap<FloodApplicationOverviewEntity, GetApplicationOverviewQueryViewModel>();
        CreateMap<FloodFundingAgencyEntity, FloodFundingAgencyViewModel>();
    }
}
