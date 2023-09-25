namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropReleaseOfFundsQueryMappingProfile : Profile
{
    public GetPropReleaseOfFundsQueryMappingProfile()
    { 
        CreateMap<FloodPropReleaseOfFundsEntity, GetPropReleaseOfFundsQueryViewModel>();
    }
}
