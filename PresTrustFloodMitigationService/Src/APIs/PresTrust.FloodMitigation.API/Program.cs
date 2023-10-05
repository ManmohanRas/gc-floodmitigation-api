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

PresTrust.FloodMitigation.Application.BackgroundJobs.GrantExpirationReminder backgroundJob = new PresTrust.FloodMitigation.Application.BackgroundJobs.GrantExpirationReminder();

RecurringJob.AddOrUpdate("Send Mail : Runs Every 1 Min", () => backgroundJob.SendEmail("RecurringJob", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")), builder.Configuration["CronTime"]);

builder.MapControllers();
builder.Run();
