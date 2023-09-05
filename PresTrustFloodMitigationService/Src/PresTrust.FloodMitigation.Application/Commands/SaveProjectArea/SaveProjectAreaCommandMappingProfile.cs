namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class SaveProjectAreaCommandMappingProfile : Profile
{
    public SaveProjectAreaCommandMappingProfile()
    {
        CreateMap<SaveProjectAreaCommand, FloodApplicationEntity>();
        CreateMap<FloodApplicationEntity, SaveProjectAreaCommandViewModel>();
    }
}
