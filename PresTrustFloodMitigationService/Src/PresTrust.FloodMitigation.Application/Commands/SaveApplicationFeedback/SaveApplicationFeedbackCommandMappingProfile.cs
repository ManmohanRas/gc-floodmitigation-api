namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationFeedbackCommandMappingProfile : Profile
{
    public SaveApplicationFeedbackCommandMappingProfile()
    {
        CreateMap<SaveApplicationFeedbackCommand, FloodApplicationFeedbackEntity>()
            .ForMember(dest => dest.Section, opt => opt.MapFrom(src => (ApplicationSectionEnum)Enum.Parse(typeof(ApplicationSectionEnum), src.Section, true)));
    }
}
