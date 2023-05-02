namespace PresTrust.FloodMitigation.API.DependencyInjection
{
    public class RegisterMediatR : IDependencyInjectionService
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.Load("PresTrust.FloodMitigation.Application"));
            });
            // Register MediatR pipeline behaviors, in the same order the behaviors should be called.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        }
    }
}
