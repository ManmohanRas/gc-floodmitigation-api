namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApplicationCommandMappingProfile : Profile
{
    public SubmitApplicationCommandMappingProfile()
    { 
        CreateMap<FloodBrokenRuleEntity, ApplicationBrokenRuleViewModel>();
    }
}
