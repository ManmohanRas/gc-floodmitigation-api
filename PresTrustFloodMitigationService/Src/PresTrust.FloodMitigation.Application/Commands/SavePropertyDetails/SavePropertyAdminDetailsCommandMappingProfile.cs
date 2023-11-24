namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyAdminDetailsCommandMappingProfile : Profile
{
    public SavePropertyAdminDetailsCommandMappingProfile()
    {
        CreateMap<SavePropertyAdminDetailsCommand, FloodPropertyAdminDetailsEntity>();
        CreateMap<SavePropertyAdminDetailsCommand, FloodParcelSoftCostEntity>();
    }
}
