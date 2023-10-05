namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationReleaseOfFundsQueryMappingProfile : Profile
{
    public GetApplicationReleaseOfFundsQueryMappingProfile()
    {
        CreateMap<FloodApplicationReleaseOfFundsEntity, GetApplicationReleaseOfFundsQueryViewModel>();
    }
}
