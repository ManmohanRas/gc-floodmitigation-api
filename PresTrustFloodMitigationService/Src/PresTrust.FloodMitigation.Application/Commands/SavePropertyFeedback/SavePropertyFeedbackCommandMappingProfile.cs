namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyFeedbackCommandMappingProfile: Profile
{
    public SavePropertyFeedbackCommandMappingProfile()
    {
        CreateMap<SavePropertyFeedbackCommand, FloodPropertyFeedbackEntity>()
            .ForMember(dest => dest.Section, opt => opt.MapFrom(src => (PropertySectionEnum)Enum.Parse(typeof(PropertySectionEnum), src.Section, true)));
    }
}
