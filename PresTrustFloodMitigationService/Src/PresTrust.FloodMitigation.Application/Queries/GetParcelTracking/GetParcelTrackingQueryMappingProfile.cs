namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelTrackingQueryMappingProfile : Profile
{
    public GetParcelTrackingQueryMappingProfile()
    {
        CreateMap<FloodParcelTrackingEntity, GetParcelTrackingQueryViewModel>();
    }
}
