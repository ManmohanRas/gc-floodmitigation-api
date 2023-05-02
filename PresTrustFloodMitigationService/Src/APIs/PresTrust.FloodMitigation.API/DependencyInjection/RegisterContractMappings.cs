namespace PresTrust.FloodMitigation.API.DependencyInjection
{
    public class RegisterContractMappings : IDependencyInjectionService
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddHttpContextAccessor();
        }
    }
}
