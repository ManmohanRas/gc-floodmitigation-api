namespace PresTrust.FloodMitigation.Application.Commands.CreateApplication;

/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class CreateApplicationCommandMappingProfile: Profile
{
    public CreateApplicationCommandMappingProfile()
    {
        CreateMap<CreateApplicationCommand, FloodApplicationEntity>();
    }
}
