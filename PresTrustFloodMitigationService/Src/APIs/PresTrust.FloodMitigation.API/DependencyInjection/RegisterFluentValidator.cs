namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterFluentValidator : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.Load("PresTrust.FloodMitigation.Application"));
    }
}
