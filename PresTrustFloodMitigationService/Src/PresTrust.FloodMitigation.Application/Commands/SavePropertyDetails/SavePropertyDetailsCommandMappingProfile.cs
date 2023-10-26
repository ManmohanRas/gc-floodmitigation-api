namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyDetailsCommandMappingProfile : Profile
{
    public SavePropertyDetailsCommandMappingProfile()
    {
        CreateMap<SavePropertyDetailsCommand, FloodPropertyAdminDetailsEntity>();
    }
}
