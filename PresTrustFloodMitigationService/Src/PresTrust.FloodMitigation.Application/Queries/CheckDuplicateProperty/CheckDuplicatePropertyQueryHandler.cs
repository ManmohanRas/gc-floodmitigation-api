namespace PresTrust.FloodMitigation.Application.Queries;

public class CheckDuplicatePropertyQueryHandler : IRequestHandler<CheckDuplicatePropertyQuery, bool>
{
    private readonly IParcelRepository repoParcel;

    public CheckDuplicatePropertyQueryHandler(
        IParcelRepository repoParcel
       )
    {
        this.repoParcel = repoParcel;
    }
    public async Task<bool> Handle(CheckDuplicatePropertyQuery request, CancellationToken cancellationToken)
    {
        var result = await repoParcel.CheckDuplicateProperty(request.Id, request.PamsPin);
        return result;
    }
}
