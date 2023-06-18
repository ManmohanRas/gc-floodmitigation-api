namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterConfigSections : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.Configure<SystemParameterConfiguration>(config.GetSection(FloodMitigationDomainConstants.AppSettingKeys.SYSTEM_PARAMETER_SECTION));
    }
}
