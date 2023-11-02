namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationStatusLogQueryMappingProfile : Profile
{
    public GetApplicationStatusLogQueryMappingProfile()
    {
        CreateMap<FloodApplicationStatusLogEntity, GetApplicationStatusLogQueryViewModel>();
    }
}
