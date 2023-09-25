namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationFundingAgencyCommandMappingProfile: Profile
{
    public SaveApplicationFundingAgencyCommandMappingProfile()
    {
        CreateMap<SaveApplicationFundingAgencyCommand, FloodApplicationFundingAgencyEntity>();
    }
}
