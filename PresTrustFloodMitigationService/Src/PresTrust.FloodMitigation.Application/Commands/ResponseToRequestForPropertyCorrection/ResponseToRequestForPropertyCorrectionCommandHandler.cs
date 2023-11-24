namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class ResponseToRequestForPropertyCorrectionCommandHandler : BaseHandler, IRequestHandler<ResponseToRequestForPropertyCorrectionCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackPropRepository repoFeedback;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public ResponseToRequestForPropertyCorrectionCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            IFeedbackPropRepository repoFeedback
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoFeedback = repoFeedback;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(ResponseToRequestForPropertyCorrectionCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        //AuthorizationCheck(application);

        // get feedback where the status is "Request Sent"
        var corrections = await repoFeedback.GetPropertyFeedbackAsync(application.Id, request.PamsPin, PropertyCorrectionStatusEnum.REQUEST_SENT.ToString());

        // update feedback status as response received and send email to an applicant
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            foreach (var section in request.Sections)
            {
                Enum.TryParse(value: section, ignoreCase: true, out PropertySectionEnum enumSection);
                await repoFeedback.ResponseToRequestForPropertyCorrectionAsync(application.Id, request.PamsPin, (int)enumSection);
            }

            // If reponse's description/feedback is not empty
            if (!string.IsNullOrEmpty(request.Feedback))
            {
                var feedback = new FloodPropertyFeedbackEntity()
                {
                    Id = 0,
                    ApplicationId = application.Id,
                    PamsPin = request.PamsPin,
                    CorrectionStatus = PropertyCorrectionStatusEnum.NONE.ToString(),
                    Feedback = request.Feedback,
                    Section = PropertySectionEnum.NONE,
                    RequestForCorrection = false,
                    LastUpdatedBy = userContext.Name
                };

                await repoFeedback.SavePropertyFeedbackAsync(feedback);
            }

            // TODO: Email
            scope.Complete();
        };

        return true;
    }

    /// <summary>
    /// Ensure that a user has the relevant authorizations to perform an action
    /// </summary>
    private void AuthorizationCheck(FloodApplicationEntity application)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION);
    }

}
