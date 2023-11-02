namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyAdminDetailsQueryMappingProfile : Profile
{
    public GetPropertyAdminDetailsQueryMappingProfile()
    {
        CreateMap<FloodPropertyAdminDetailsEntity, GetPropertyAdminDetailsQueryViewModel>();
    }
}
