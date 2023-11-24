namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkPropertyFeedbackAsReadCommandHandler : IRequestHandler<MarkPropertyFeedbackAsReadCommand, bool>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IFeedbackPropRepository repoFeedback;

    public MarkPropertyFeedbackAsReadCommandHandler(IMapper mapper,
        IPresTrustUserContext userContext,
        IFeedbackPropRepository repoFeedback
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFeedback = repoFeedback;
    }

    public async Task<bool> Handle(MarkPropertyFeedbackAsReadCommand request, CancellationToken cancellationToken)
    {
        if (userContext.Role == UserRoleEnum.PROGRAM_ADMIN)
        {
            await repoFeedback.MarkPropertyFeedbackAsReadAsync(request.FeedbackIds);
        }
        return true;
    }
}
