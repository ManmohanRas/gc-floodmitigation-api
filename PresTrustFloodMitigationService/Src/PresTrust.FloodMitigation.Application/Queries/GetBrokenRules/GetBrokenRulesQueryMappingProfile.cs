namespace PresTrust.FloodMitigation.Application.Queries;

public class GetBrokenRulesQueryMappingProfile: Profile
{
    public GetBrokenRulesQueryMappingProfile()
    {
        CreateMap<FloodBrokenRuleEntity, GetBrokenRulesQueryViewModel>();
    }
}
