namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, CreateApplicationCommandViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    public CreateApplicationCommandHandler(
        IMapper  mapper,
        IApplicationRepository repoApplication
        ) 
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
    }
    public async Task<CreateApplicationCommandViewModel> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var reqApplication = mapper.Map<CreateApplicationCommand, FloodApplicationEntity>(request);
        reqApplication.Status = ApplicationStatusEnum.DOI_DRAFT;

        reqApplication = await repoApplication.SaveAsync(reqApplication);
        var result = mapper.Map<FloodApplicationEntity, CreateApplicationCommandViewModel>(reqApplication);

        return result;
    }
}
