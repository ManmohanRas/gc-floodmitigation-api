namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFeedbacksQueryMappingProfile: Profile
{
    public GetFeedbacksQueryMappingProfile() 
    {
        CreateMap<FlmitigFeedbackEntity, GetFeedbacksQueryViewModel>()
        .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section.ToString()));
    }
}
