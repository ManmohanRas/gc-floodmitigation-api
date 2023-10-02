namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropFeedbackCommandMappingProfile: Profile
{
    public SavePropFeedbackCommandMappingProfile()
    {
        CreateMap<SavePropFeedbackCommand, FloodPropertyFeedbackEntity>()
            .ForMember(dest => dest.Section, opt => opt.MapFrom(src => (ApplicationSectionEnum)Enum.Parse(typeof(ApplicationSectionEnum), src.Section, true)));
    }
}
