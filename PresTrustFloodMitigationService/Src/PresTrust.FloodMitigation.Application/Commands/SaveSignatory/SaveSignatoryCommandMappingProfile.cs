namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSignatoryCommandMappingProfile : Profile
{
    public SaveSignatoryCommandMappingProfile()
    {
        CreateMap<SaveSignatoryCommand, FloodSignatoryEntity>();
    }
}
