namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactsCommandMappingProfile : Profile
{
    public SaveContactsCommandMappingProfile()
    {
        CreateMap<SaveContactsModel, FloodContactEntity>();
    }
}
