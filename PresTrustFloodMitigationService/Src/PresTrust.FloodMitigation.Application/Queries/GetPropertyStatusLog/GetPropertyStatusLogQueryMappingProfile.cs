namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyStatusLogQueryMappingProfile : Profile
{
    public GetPropertyStatusLogQueryMappingProfile()
    {
        CreateMap<FloodParcelStatusLogEntity, GetPropertyStatusLogQueryViewModel>();
    }
}
