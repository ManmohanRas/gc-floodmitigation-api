namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitPropertyCommandMappingProfile : Profile
{
    public SubmitPropertyCommandMappingProfile()
    {
        CreateMap<FloodPropertyBrokenRuleEntity, PropertyBrokenRulesViewModel>();
    }
}
