namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDocumentCommandMappingProfile: Profile
{
    public SaveFlapDocumentCommandMappingProfile() 
    {
        CreateMap<SaveFlapDocumentCommand, FloodFlapDocumentEntity>();
        CreateMap<FloodFlapDocumentEntity, SaveFlapDocumentCommandViewModel>();
    }
}
