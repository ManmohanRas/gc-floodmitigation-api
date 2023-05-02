var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Services.AddServices(applicationBuilder.Configuration);

var application = applicationBuilder.Build();
application.AddMiddleware(application.Environment);
application.MapControllers();
await application.RunAsync();
