namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationAdminDetailsQueryMappingProfile : Profile
{
    public GetApplicationAdminDetailsQueryMappingProfile()
    {
        CreateMap<FloodApplicationAdminDetailsEntity, GetApplicationAdminDetailsQueryViewModel>();
    }
}
