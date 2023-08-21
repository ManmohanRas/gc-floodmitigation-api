namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFundingAgencyCommandMappingProfile: Profile
{
    public SaveFundingAgencyCommandMappingProfile()
    {
        CreateMap<SaveFundingAgencyCommand, FloodFundingAgencyEntity>();
    }
}
