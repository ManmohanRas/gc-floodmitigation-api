namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationUsersQueryMappingProfile: Profile
{
    public GetApplicationUsersQueryMappingProfile()
    {
        CreateMap<IdentityApiUser, FloodApplicationUserViewModel>();

        CreateMap<FloodApplicationUserEntity, FloodApplicationUserViewModel>();
    }
}
