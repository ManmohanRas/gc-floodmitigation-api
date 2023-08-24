namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class DeleteFundingSourceCommandMappingProfile: Profile
{
    public DeleteFundingSourceCommandMappingProfile()
    {
        CreateMap<DeleteFundingSourceCommand, FloodFundingSourceEntity>();
    }
}
