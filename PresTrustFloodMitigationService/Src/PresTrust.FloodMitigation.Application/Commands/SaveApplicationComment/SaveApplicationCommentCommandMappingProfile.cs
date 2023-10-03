namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationCommentCommandMappingProfile : Profile
{
    public SaveApplicationCommentCommandMappingProfile() 
    {
        CreateMap<SaveApplicationCommentCommand, FloodApplicationCommentEntity>();
    }
}
