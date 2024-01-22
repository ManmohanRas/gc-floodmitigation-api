namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteMunicipalCommentCommandMappingProfile : Profile
{
    public DeleteMunicipalCommentCommandMappingProfile()
    {
        CreateMap<DeleteMunicipalCommentCommand, FloodMunicipalCommentEntity>();
    }
}
