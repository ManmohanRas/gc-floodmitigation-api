namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsForWarningsQueryMappingProfile : Profile
{
    public GetApplicationsForWarningsQueryMappingProfile()
    {
        CreateMap<FloodApplicationEntity, GetApplicationsForWarningsQueryViewModel>()
            .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src.ApplicationStatus.ToString()))
            .ForMember(dest => dest.PropertyStatus, opt => opt.MapFrom(src => src.PropertyStatus.ToString()));
    }
}
