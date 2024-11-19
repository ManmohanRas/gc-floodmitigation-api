using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveWarningsCommandHandler : BaseHandler, IRequestHandler<SaveWarningsCommand, bool>
{
    private readonly IApplicationParcelRepository repoProperty;

    public SaveWarningsCommandHandler
    (
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoProperty
    ) : base(repoApplication, repoProperty)
    {
        this.repoProperty = repoProperty;
    }

    /// <summary>
    /// 
    /// </summary>....................... 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(SaveWarningsCommand request, CancellationToken cancellationToken)
    {
        var result = false;

        if (request.WarningType == "waiting" || request.WarningType == "rejected")
        {
            var appParcel = await repoProperty.GetApplicationPropertyAsync(request.ApplicationId, request.PamsPin);
            if (appParcel != null)
            {
                if (request.WarningType == "waiting")
                {
                    appParcel.WaitingApproved = true;
                }
                else if (request.WarningType == "rejected")
                {
                    appParcel.RejectedApproved = true;
                }
                await repoProperty.UpdateApplicationParcelWarnings(appParcel);
                result = true;
            }
        }        

        return result;
    }
}
