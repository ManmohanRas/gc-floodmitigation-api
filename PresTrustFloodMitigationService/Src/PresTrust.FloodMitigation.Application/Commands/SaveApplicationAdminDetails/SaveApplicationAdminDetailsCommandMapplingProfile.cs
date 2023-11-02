namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationAdminDetailsCommandMapplingProfile : Profile
{
    public SaveApplicationAdminDetailsCommandMapplingProfile()
    {
        CreateMap<SaveApplicationAdminDetailsCommand, FloodApplicationAdminDetailsEntity>();
    }
}
