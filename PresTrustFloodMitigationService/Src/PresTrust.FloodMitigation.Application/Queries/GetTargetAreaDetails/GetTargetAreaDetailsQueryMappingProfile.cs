namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTargetAreaDetailsQueryMappingProfile: Profile
{
    public GetTargetAreaDetailsQueryMappingProfile() {
        CreateMap<FloodFlapTargetAreaEntity, GetTargetAreaDetailsQueryViewModel>();
        CreateMap<FloodParcelEntity, GetFloodFlapParcelViewModel>();
    }
}
