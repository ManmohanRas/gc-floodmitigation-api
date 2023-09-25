namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationCommentCommandMappingProfile:Profile
{
    public DeleteApplicationCommentCommandMappingProfile() 
    {
        CreateMap<DeleteApplicationCommentCommand, FloodApplicationCommentEntity>();
    }
}
