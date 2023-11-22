
namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelPropertyCommandMappingProfile : Profile
{
    public SaveParcelPropertyCommandMappingProfile()
    {
        CreateMap<SaveParcelPropertyCommand, FloodParcelPropertyEntity>();
        CreateMap<SaveParcelPropertyCommand, FloodParcelEntity>();
    }
}
