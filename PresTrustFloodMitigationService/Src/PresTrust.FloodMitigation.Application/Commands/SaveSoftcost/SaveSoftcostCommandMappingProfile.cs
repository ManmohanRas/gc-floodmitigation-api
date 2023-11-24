namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftCostCommandMappingProfile : Profile
{
    public SaveSoftCostCommandMappingProfile() 
    {
        CreateMap<SaveSoftCostModel, FloodParcelSoftCostEntity>();
        CreateMap<SaveSoftCostCommand, FloodPropReleaseOfFundsEntity>();
        CreateMap<SaveSoftCostCommand, FloodPropertyAdminDetailsEntity>();
    }
}
