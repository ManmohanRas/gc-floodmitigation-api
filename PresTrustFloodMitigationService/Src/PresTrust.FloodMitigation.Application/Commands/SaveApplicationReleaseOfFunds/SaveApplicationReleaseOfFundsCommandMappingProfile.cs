namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationReleaseOfFundsCommandMappingProfile: Profile
{
    public SaveApplicationReleaseOfFundsCommandMappingProfile()
    {
        CreateMap<SaveApplicationReleaseOfFundsCommand, FloodApplicationReleaseOfFundsEntity>();
    }
}
