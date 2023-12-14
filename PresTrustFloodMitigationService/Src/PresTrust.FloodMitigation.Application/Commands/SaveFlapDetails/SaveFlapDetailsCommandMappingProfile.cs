using PresTrust.FloodMitigation.Application.Queries;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDetailsCommandMappingProfile: Profile
{
    public SaveFlapDetailsCommandMappingProfile() 
    {
        CreateMap<SaveFlapDetailsCommand, FloodFlapEntity>();
        CreateMap<FloodFlapCommentViewModel, FloodFlapCommentEntity>();
    }
}
