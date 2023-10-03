namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationCommentsQueryMappingProfile: Profile
{
    public GetApplicationCommentsQueryMappingProfile() 
    {
        CreateMap<FloodApplicationCommentEntity, GetApplicationCommentsQueryViewModel>();
    }
}
