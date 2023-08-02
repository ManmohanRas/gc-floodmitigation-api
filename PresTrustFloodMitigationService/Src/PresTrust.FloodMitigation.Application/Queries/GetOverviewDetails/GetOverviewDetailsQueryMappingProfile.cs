namespace PresTrust.FloodMitigation.Application.Queries;

public class GetOverviewDetailsQueryMappingProfile : Profile
{
    public GetOverviewDetailsQueryMappingProfile() 
    {
        CreateMap<FloodOverviewDetailsEntity, GetOverviewDetailsQueryViewModel>();
    }
}
