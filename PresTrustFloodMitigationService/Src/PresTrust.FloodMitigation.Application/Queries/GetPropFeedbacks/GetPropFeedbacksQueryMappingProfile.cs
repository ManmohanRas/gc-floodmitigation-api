namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropFeedbacksQueryMappingProfile: Profile
{
    public GetPropFeedbacksQueryMappingProfile()
    {
        CreateMap<FloodPropFeedbackEntity, GetPropFeedbacksQueryViewModel>()
        .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section.ToString()));
    }
}
