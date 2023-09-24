namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationFundingAgencyCommandMappingProfile: Profile
{
    public DeleteApplicationFundingAgencyCommandMappingProfile()
    {
        CreateMap<DeleteApplicationFundingAgencyCommand, FloodApplicationFundingAgencyEntity>();
    }
}
