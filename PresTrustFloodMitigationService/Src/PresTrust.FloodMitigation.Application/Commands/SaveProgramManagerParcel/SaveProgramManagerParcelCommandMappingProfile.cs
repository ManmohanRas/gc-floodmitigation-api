
namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramManagerParcelCommandMappingProfile : Profile
{
    public SaveProgramManagerParcelCommandMappingProfile()
    {
        CreateMap<SaveProgramManagerParcelCommand, FloodParcelEntity>();
    }
}
