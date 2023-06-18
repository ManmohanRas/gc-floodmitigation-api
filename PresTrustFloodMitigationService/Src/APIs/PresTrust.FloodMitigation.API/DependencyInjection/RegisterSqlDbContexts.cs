namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterSqlDbContexts : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<PresTrustSqlDbContext>();
    }
}
