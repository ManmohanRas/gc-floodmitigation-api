namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveOverviewDetailsCommandMappingProfile: Profile
{
    public SaveOverviewDetailsCommandMappingProfile()
    {
        CreateMap<SaveOverviewDetailsCommand, FloodOverviewDetailsEntity>();
    }
}
