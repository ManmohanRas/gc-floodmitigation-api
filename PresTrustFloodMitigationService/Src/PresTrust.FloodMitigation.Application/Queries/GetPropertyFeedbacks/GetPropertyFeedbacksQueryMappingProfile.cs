namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyFeedbacksQueryMappingProfile: Profile
{
    public GetPropertyFeedbacksQueryMappingProfile()
    {
        CreateMap<FloodPropertyFeedbackEntity, GetPropertyFeedbacksQueryViewModel>()
        .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section.ToString()));
    }
}
