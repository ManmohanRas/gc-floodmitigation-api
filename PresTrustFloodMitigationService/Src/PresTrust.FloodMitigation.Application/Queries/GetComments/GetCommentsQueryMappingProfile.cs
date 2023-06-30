namespace PresTrust.FloodMitigation.Application.Queries;

public class GetCommentsQueryMappingProfile: Profile
{
    public GetCommentsQueryMappingProfile() 
    {
        CreateMap<FloodCommentEntity, GetCommentsQueryViewModel>();
    }
}
