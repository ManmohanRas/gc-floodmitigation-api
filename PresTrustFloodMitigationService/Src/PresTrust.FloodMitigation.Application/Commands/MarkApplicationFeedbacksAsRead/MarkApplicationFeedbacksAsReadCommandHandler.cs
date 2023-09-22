namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkApplicationFeedbacksAsReadCommandHandler : IRequestHandler<MarkApplicationFeedbacksAsReadCommand, bool>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationFeedbackRepository repoFeedback;

    public MarkApplicationFeedbacksAsReadCommandHandler(IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationFeedbackRepository repoFeedback
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFeedback = repoFeedback;
    }

    public async Task<bool> Handle(MarkApplicationFeedbacksAsReadCommand request, CancellationToken cancellationToken)
    {
        if (userContext.Role == UserRoleEnum.PROGRAM_ADMIN)
        {
            await repoFeedback.MarkFeedbacksAsReadAsync(request.FeedbackIds);
        }
        return true;
    }
}
