namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelPropertyQueryMappingProfile: Profile
{

    public GetParcelPropertyQueryMappingProfile()
    {
        CreateMap<FloodParcelPropertyEntity, GetParcelPropertyQueryViewModel>();
    }
}
