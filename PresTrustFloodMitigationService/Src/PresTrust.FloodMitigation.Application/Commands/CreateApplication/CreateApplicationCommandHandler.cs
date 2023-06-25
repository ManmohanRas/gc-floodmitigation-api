using System.Security.Policy;

namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class CreateApplicationCommandHandler : BaseHandler, IRequestHandler<CreateApplicationCommand, CreateApplicationCommandViewModel>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationRepository repoApplication;

    public CreateApplicationCommandHandler(
        IMapper  mapper,
        IPresTrustUserContext userContext,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoApplication = repoApplication;
    }

    public async Task<CreateApplicationCommandViewModel> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        AuthorizationCheck(request);

        var reqApplication = mapper.Map<CreateApplicationCommand, FloodApplicationEntity>(request);
        reqApplication.Status = ApplicationStatusEnum.DECLARATION_OF_INTENT_DRAFT;
        reqApplication.LastUpdatedBy = userContext.Email;
        reqApplication = await repoApplication.SaveAsync(reqApplication);
        
        var result = mapper.Map<FloodApplicationEntity, CreateApplicationCommandViewModel>(reqApplication);
        return result;
    }

    private void AuthorizationCheck(CreateApplicationCommand request)
    {
        userContext.DeriveRole(request.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, applicationStatus: ApplicationStatusEnum.NONE, operation: UserPermissionEnum.CREATE_APPLICATION);
    }
}
