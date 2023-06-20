namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteCommentCommandMappingProfile:Profile
{
    public DeleteCommentCommandMappingProfile() 
    {
        CreateMap<DeleteCommentCommand, FloodCommentsEntity>();
    }
}
