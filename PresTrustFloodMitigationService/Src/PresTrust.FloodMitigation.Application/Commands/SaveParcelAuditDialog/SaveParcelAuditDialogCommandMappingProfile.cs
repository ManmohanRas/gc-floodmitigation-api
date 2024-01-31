namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelAuditDialogCommandMappingProfile: Profile
{
    public SaveParcelAuditDialogCommandMappingProfile()
    {
        CreateMap<SaveParcelAuditDialogCommand, FloodParcelAuditDialogEntity>();
    }
}
