namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftCostCommandMappingProfile : Profile
{
    public SaveSoftCostCommandMappingProfile() 
    {
        CreateMap<SaveSoftCostModel, FloodParcelSoftCostEntity>();
    }
}
