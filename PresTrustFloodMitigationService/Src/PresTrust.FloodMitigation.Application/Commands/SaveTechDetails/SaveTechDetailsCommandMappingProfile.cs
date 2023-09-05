namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTechDetailsCommandMappingProfile : Profile
{
    public SaveTechDetailsCommandMappingProfile()
        {
        CreateMap<SaveTechDetailsCommand, FloodTechDetailsEntity>();
    }
}
