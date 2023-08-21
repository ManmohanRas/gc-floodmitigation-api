namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFundingAgencyCommandMappingProfile: Profile
{
    public DeleteFundingAgencyCommandMappingProfile()
    {
        CreateMap<DeleteFundingAgencyCommand, FloodFundingAgencyEntity>();
    }
}
