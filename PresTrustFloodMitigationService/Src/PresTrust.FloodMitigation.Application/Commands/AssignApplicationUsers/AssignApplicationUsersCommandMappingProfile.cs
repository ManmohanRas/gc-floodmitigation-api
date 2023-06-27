namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class AssignApplicationUsersCommandMappingProfile: Profile
{
    public AssignApplicationUsersCommandMappingProfile()
    {
        CreateMap<FloodApplicationUserViewModel, FloodApplicationUserEntity>();
    }
}
