namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalFinanceCommandMappingProfile: Profile
{
    public SaveMunicipalFinanceCommandMappingProfile() 
    {
        CreateMap<SaveMunicipalFinanceCommand, FloodMunicipalFinanceEntity>();
    }
}
