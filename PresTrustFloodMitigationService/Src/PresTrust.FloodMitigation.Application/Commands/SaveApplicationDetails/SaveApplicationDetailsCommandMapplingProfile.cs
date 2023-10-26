namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationDetailsCommandMapplingProfile : Profile
{
    public SaveApplicationDetailsCommandMapplingProfile()
    {
        CreateMap<SaveApplicationDetailsCommand, FloodApplicationDetailsEntity>();
    }
}
