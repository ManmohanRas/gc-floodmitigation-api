namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class defines the configuration using profiles.
/// </summary>
public class SaveApplicationFinanceCommandMappingProfile: Profile
{
    public SaveApplicationFinanceCommandMappingProfile()
    {
        CreateMap<SaveApplicationFinanceCommand, FloodApplicationFinanceEntity> ();
        CreateMap<FloodFundingSourceViewModel, FloodFundingSourceEntity>();
        CreateMap<FloodFinanceLineItemViewModel, FloodParcelFinanceEntity>();
    }
}
