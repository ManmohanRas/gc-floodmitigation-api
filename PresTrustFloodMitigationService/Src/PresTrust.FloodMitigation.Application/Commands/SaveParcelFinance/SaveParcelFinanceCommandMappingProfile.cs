namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelFinanceCommandMappingProfile: Profile
{
    public SaveParcelFinanceCommandMappingProfile()
    {
        CreateMap<SaveParcelFinanceCommand, FloodParcelFinanceEntity>();
    }
}
