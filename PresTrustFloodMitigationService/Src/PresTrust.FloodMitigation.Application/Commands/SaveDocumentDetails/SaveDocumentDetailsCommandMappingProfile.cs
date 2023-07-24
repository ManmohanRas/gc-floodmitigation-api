namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class SaveDocumentDetailsCommandMappingProfile: Profile
{
    public SaveDocumentDetailsCommandMappingProfile() 
    {
        CreateMap<SaveDocumentDetailsCommand, FloodDocumentEntity>();
        CreateMap<FloodDocumentEntity, SaveDocumentDetailsCommandViewModel>();
    }
}
