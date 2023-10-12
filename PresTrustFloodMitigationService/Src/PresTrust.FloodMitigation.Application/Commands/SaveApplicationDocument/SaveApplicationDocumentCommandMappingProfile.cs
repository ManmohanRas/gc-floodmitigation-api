namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class SaveApplicationDocumentCommandMappingProfile: Profile
{
    public SaveApplicationDocumentCommandMappingProfile() 
    {
        CreateMap<SavePropertyDocumentDetailsCommand, FloodPropertyDocumentEntity>();
        CreateMap<FloodPropertyDocumentEntity, SavePropertyDocumentDetailsCommandViewModel>();
    }
}
