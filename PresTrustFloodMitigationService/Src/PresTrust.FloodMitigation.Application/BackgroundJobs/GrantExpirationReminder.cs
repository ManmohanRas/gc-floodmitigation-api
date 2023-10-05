using MediatR;

namespace PresTrust.FloodMitigation.Application.BackgroundJobs;

public class GrantExpirationReminder : BaseHandler, IGrantExpirationReminder
{
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;

    //public GrantExpirationReminder
    //(
    //       IOptions<SystemParameterConfiguration> systemParamOptions,
    //       IApplicationRepository repoApplication
    //) :base(repoApplication)
    //{
    //    this.systemParamOptions = systemParamOptions.Value;
    //    this.repoApplication = repoApplication;
    //}

    public void SendEmail(string backGroundJobType, string startTime)
    {
        Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
    }

    public async Task Handle()
    {
        // Get list of applications with properties that will expire in 3 months on Grant Expiration Date. 

        // Iterate through each application and fetch properties that will expire in 3 months on Grant Expiration Date.

            // Get Email template

            // Send Email
    }
}

 