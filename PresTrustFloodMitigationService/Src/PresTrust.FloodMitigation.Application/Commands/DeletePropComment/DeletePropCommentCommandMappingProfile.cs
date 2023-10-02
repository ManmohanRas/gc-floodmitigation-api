namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropCommentCommandMappingProfile: Profile
{
    public DeletePropCommentCommandMappingProfile()
    {
        CreateMap<DeletePropCommentCommand, FloodPropertyCommentEntity>();
    }
}
