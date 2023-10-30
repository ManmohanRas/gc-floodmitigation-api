namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationUsersQueryMappingProfile: Profile
{
    public GetApplicationUsersQueryMappingProfile()
    {
        CreateMap<IdentityApiUser, FloodApplicationUserViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRole));

        CreateMap<FloodApplicationUserEntity, FloodApplicationUserViewModel>();
        CreateMap<FloodApplicationUserViewModel, FloodApplicationUserEntity>();
    }
}
