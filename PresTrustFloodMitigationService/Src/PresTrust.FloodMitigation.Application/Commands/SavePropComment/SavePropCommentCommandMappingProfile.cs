namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropCommentCommandMappingProfile : Profile
{
    public SavePropCommentCommandMappingProfile()
    {
        CreateMap<SaveApplicationCommentCommand, FloodPropCommentEntity>();
    }
}
