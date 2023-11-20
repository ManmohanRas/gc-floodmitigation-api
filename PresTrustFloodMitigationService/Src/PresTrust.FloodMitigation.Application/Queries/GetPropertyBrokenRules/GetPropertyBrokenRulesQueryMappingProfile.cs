namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyBrokenRulesQueryMappingProfile: Profile
{
    public GetPropertyBrokenRulesQueryMappingProfile()
    {
        CreateMap<FloodPropertyBrokenRuleEntity, GetPropertyBrokenRulesQueryViewModel>();
    }
}
