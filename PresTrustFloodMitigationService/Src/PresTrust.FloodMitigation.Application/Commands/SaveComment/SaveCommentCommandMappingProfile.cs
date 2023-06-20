namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveCommentCommandMappingProfile : Profile
{
    public SaveCommentCommandMappingProfile() 
    {
        CreateMap<SaveCommentCommand, FloodCommentsEntity>();
    }
}
