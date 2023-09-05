namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftcostCommandMappingProfile : Profile
{
    public SaveSoftcostCommandMappingProfile() 
    {
        CreateMap<FloodParcelSoftcostViewModel, FloodParcelSoftcostEntity>();
    }
}
