namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterAutoMapper : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(Assembly.Load("PresTrust.FloodMitigation.Application"));
    }
}
