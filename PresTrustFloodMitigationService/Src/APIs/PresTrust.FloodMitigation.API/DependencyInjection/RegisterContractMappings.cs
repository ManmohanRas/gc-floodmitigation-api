namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterContractMappings : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IPresTrustUserContext, PresTrustUserContext>();
        services.AddTransient<ICoreRepository, CoreRepository>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddTransient<IApplicationParcelRepository, ApplicationParcelRepository>();
        services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddTransient<IFeedbackRepository, FeedbackRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddHttpContextAccessor();
    }
}                                                                               
