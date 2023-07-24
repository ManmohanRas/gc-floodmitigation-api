namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationPropertiesQueryMappingProfile : Profile
{
    public GetApplicationPropertiesQueryMappingProfile()
    {
        CreateMap<FloodParcelEntity, GetApplicationPropertiesQueryViewModel>();
    }
}
