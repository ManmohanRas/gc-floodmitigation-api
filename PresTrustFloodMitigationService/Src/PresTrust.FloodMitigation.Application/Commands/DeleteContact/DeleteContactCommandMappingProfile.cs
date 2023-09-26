namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteContactCommandMappingProfile : Profile
{
    public DeleteContactCommandMappingProfile()
    {
        CreateMap<DeleteContactCommand, FloodContactEntity>();
    }
}
