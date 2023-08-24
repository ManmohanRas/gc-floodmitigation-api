namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropCommentsQueryMappingProfile: Profile
{
    public GetPropCommentsQueryMappingProfile()
    {
        CreateMap<FloodPropCommentEntity, GetPropCommentsQueryViewModel>();
    }
}
