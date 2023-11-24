namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyCommentsQueryMappingProfile: Profile
{
    public GetPropertyCommentsQueryMappingProfile()
    {
        CreateMap<FloodPropertyCommentEntity, GetPropertyCommentsQueryViewModel>();
    }
}
