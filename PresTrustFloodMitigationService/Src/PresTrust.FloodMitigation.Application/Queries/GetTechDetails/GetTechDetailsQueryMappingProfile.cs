namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTechDetailsQueryMappingProfile: Profile
{

    public GetTechDetailsQueryMappingProfile()
    {
        CreateMap<FloodTechDetailsEntity, GetTechDetailsQueryViewModel>();
    }
}
