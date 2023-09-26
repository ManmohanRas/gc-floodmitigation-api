namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationOverviewCommandMappingProfile: Profile
{
    public SaveApplicationOverviewCommandMappingProfile()
    {
        CreateMap<SaveApplicationOverviewCommand, FloodApplicationOverviewEntity>();
    }
}
