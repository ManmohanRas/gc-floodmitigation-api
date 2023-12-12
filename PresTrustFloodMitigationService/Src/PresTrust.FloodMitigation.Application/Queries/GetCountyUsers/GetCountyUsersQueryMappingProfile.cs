namespace PresTrust.FloodMitigation.Application.Queries;

public class GetCountyUsersQueryMappingProfile: Profile
{
    public GetCountyUsersQueryMappingProfile()
    {
        CreateMap<IdentityApiUser, PresTrustUserEntity>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRole));
    }
}
