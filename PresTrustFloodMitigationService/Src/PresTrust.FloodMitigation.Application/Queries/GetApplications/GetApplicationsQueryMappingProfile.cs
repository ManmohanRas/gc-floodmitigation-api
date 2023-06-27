namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsQueryMappingProfile : Profile
{
    public GetApplicationsQueryMappingProfile()
    {
        CreateMap<FloodApplicationEntity, GetApplicationsQueryViewModel>()
            .ForMember(dest => dest.ApplicationType, opt => opt.MapFrom(src => src.ApplicationType.ToString()))
            .ForMember(dest => dest.ApplicationSubType, opt => opt.MapFrom(src => src.ApplicationSubType.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
