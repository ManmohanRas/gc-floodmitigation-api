using Hangfire;
using Hangfire.SqlServer;
using System.Configuration;
using System;

var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Services.AddServices(applicationBuilder.Configuration);

var sqlServerStorageOptions = new SqlServerStorageOptions
{
    PrepareSchemaIfNecessary = false
};

applicationBuilder.Services.AddHangfire(x => x.UseSqlServerStorage(applicationBuilder.Configuration.GetConnectionString("HangfireDbConnectionStrings"), sqlServerStorageOptions));
applicationBuilder.Services.AddHangfireServer();

applicationBuilder.Services.AddEndpointsApiExplorer();
var builder = applicationBuilder.Build();
builder.AddMiddleware(builder.Environment);

builder.MapHangfireDashboard();
builder.UseHangfireDashboard("/backgroundjobs");


var jobGrantExpirationReminder = builder.Services.GetService<IGrantExpirationReminder>();
//RecurringJob.AddOrUpdate("Send Mail : Runs Every 1 Min", () => jobGrantExpirationReminder.SendEmail("RecurringJob", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")), builder.Configuration["CronTime"]);

RecurringJob.AddOrUpdate("Send Mail : Runs Every 5 Min", () => jobGrantExpirationReminder.Handle(), builder.Configuration["CronTimeGrantExpirationReminder"]);

builder.MapControllers();
builder.Run();
