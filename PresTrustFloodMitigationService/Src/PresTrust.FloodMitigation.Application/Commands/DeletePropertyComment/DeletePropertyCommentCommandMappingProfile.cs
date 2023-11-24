namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropertyCommentCommandMappingProfile: Profile
{
    public DeletePropertyCommentCommandMappingProfile()
    {
        CreateMap<DeletePropertyCommentCommand, FloodPropertyCommentEntity>();
    }
}
