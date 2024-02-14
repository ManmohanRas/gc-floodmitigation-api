namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryItemCommandMappingProfile: Profile
{
    public SaveParcelHistoryItemCommandMappingProfile()
    {
        CreateMap<SaveParcelHistoryItemCommand, FloodParcelHistoryEntity>();
    }
}
