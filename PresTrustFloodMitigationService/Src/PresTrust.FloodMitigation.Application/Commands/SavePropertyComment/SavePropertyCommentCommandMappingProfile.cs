namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyCommentCommandMappingProfile : Profile
{
    public SavePropertyCommentCommandMappingProfile()
    {
        CreateMap<SavePropertyCommentCommand, FloodPropertyCommentEntity>();
    }
}
