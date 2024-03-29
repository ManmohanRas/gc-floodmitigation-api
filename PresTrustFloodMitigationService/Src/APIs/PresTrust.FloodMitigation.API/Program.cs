using Hangfire;
using Hangfire.SqlServer;
using System.Configuration;
using System;
using System.Runtime.InteropServices;

var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Services.AddServices(applicationBuilder.Configuration);

var sqlServerStorageOptions = new SqlServerStorageOptions
{
    PrepareSchemaIfNecessary = true,
    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    QueuePollInterval = TimeSpan.Zero,
    UseRecommendedIsolationLevel = true,
    DisableGlobalLocks = true,
};

applicationBuilder.Services.AddHangfire(x => x.UseSqlServerStorage(applicationBuilder.Configuration.GetConnectionString("HangfireDbConnectionStrings"), sqlServerStorageOptions));
applicationBuilder.Services.AddHangfireServer();

applicationBuilder.Services.AddEndpointsApiExplorer();
//CACHE
applicationBuilder.Services.AddMemoryCache();

var builder = applicationBuilder.Build();
builder.AddMiddleware(builder.Environment);

builder.MapHangfireDashboard();
builder.UseHangfireDashboard("/backgroundjobs");

//var jobGrantExpirationReminder = builder.Services.GetService<IGrantExpirationReminder>();
var jobProjectAreaExpirationReminder = builder.Services.GetService<IProjectAreaExpirationReminder>();

var jobOptions = new RecurringJobOptions()
{
    TimeZone = TimeZoneInfo.Local    
};

//RecurringJob.AddOrUpdate("Reminder: Grant Expiration", () => jobGrantExpirationReminder.Handle(), builder.Configuration["CronTimeGrantExpirationReminder"], jobOptions);
RecurringJob.AddOrUpdate("Reminder: Project Area Expiration", () => jobProjectAreaExpirationReminder.Handle(), builder.Configuration["CronTimeGrantExpirationReminder"], jobOptions);

builder.MapControllers();
builder.Run();
