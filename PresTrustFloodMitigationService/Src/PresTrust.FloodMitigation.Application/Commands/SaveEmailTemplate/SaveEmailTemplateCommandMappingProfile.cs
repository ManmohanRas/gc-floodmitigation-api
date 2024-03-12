namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveEmailTemplateCommandMappingProfile: Profile
{
    public SaveEmailTemplateCommandMappingProfile()
    {
        CreateMap<SaveEmailTemplateCommand, FloodEmailTemplateEntity>();
    }
}
