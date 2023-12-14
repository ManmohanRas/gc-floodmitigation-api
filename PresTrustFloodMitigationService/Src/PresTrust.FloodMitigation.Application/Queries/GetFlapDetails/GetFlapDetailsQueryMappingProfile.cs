namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQueryMappingProfile: Profile
{
    public GetFlapDetailsQueryMappingProfile()
    {
        CreateMap<FloodFlapEntity, GetFlapDetailsQueryViewModel>();
    }
}
