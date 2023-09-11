namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactCommandMappingProfile : Profile
{
    public SaveContactCommandMappingProfile()
    {
        CreateMap<SaveContactCommand, FloodContactEntity>();
    }
}
