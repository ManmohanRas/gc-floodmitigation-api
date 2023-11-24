namespace PresTrust.FloodMitigation.Application.Commands;

public class PendingPropertyCommandMappingProfile : Profile
{
    public PendingPropertyCommandMappingProfile()
    {
        CreateMap<FloodPropertyBrokenRuleEntity, PropertyBrokenRulesViewModel>();
    }
}
