namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFeedbackCommandMappingProfile : Profile
{
    public SaveFeedbackCommandMappingProfile()
    {
        CreateMap<SaveFeedbackCommand, FlmitigFeedbackEntity>()
            .ForMember(dest => dest.Section, opt => opt.MapFrom(src => (ApplicationSectionEnum)Enum.Parse(typeof(ApplicationSectionEnum), src.Section, true)));
    }
}
