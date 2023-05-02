namespace PresTrust.FloodMitigation.API.Contracts
{
    public interface IDependencyInjectionService
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
