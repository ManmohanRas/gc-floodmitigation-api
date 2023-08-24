namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkPropFeedbackAsReadCommandHandler : BaseHandler, IRequestHandler<MarkPropFeedbackAsReadCommand, bool>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IFeedbackPropRepository repoFeedback;

    public MarkPropFeedbackAsReadCommandHandler(IMapper mapper,
        IPresTrustUserContext userContext,
        IFeedbackPropRepository repoFeedback
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFeedback = repoFeedback;
    }

    public async Task<bool> Handle(MarkPropFeedbackAsReadCommand request, CancellationToken cancellationToken)
    {
        if (userContext.Role == UserRoleEnum.PROGRAM_ADMIN)
        {
            await repoFeedback.MarkPropFeedbackAsReadAsync(request.FeedbackIds);
        }
        return true;
    }
}
