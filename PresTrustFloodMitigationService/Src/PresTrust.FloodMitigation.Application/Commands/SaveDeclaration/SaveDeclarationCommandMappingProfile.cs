namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDeclarationCommandMappingProfile : Profile
{
    public SaveDeclarationCommandMappingProfile()
    {
        CreateMap<SaveDeclarationCommand, FloodApplicationEntity>();
        CreateMap<FloodApplicationUserViewModel, FloodApplicationUserEntity>();
    }
}