namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationSignatoryCommandMappingProfile : Profile
{
    public SaveApplicationSignatoryCommandMappingProfile()
    {
        CreateMap<SaveApplicationSignatoryCommand, FloodApplicationSignatoryEntity>();
    }
}
