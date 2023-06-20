using PresTrust.FloodMitigation.Application;

namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterContractMappings : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ITestRepository, TestRepository>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddTransient<IFeedbackRepository, FeedbackRepository>();
        services.AddSingleton<IPresTrustUserContext, PresTrustUserContext>();
        services.AddHttpContextAccessor();

    }
}                                                                               
