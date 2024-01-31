namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryCommandMappingProfile: Profile
{
    public SaveParcelHistoryCommandMappingProfile()
    {
        CreateMap<SaveParcelHistoryCommand, FloodParcelHistoryEntity>();
    }
}
