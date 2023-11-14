namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteParcelSoftCostCommandMappingProfile: Profile
{
    public DeleteParcelSoftCostCommandMappingProfile() 
    {
        CreateMap<DeleteParcelSoftCostCommand, FloodParcelSoftCostEntity>();
    }  
}
