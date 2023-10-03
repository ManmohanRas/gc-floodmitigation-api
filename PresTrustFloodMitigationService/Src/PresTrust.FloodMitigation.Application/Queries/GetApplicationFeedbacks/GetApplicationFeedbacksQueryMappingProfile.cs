namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationFeedbacksQueryMappingProfile: Profile
{
    public GetApplicationFeedbacksQueryMappingProfile() 
    {
        CreateMap<FloodApplicationFeedbackEntity, GetApplicationFeedbacksQueryViewModel>()
        .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section.ToString()));
    }
}
