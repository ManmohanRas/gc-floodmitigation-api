namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterContractMappings : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ITestRepository, TestRepository>();
        services.AddSingleton<IPresTrustUserContext, PresTrustUserContext>();
        services.AddSingleton<ICoreRepository, CoreRepository>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddTransient<IFeedbackRepository, FeedbackRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddHttpContextAccessor();
    }
}                                                                               
