namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalTrustFundPermittedUsesCommandHandler:IRequestHandler<SaveMunicipalTrustFundPermittedUsesCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IMunicipalTrustFundPermittedUsesRepository repoMunicipalTrustFundPermittedUses;

    public SaveMunicipalTrustFundPermittedUsesCommandHandler(
            IMapper mapper,
            IPresTrustUserContext userContext,
            IMunicipalTrustFundPermittedUsesRepository repoMunicipalTrustFundPermittedUses
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoMunicipalTrustFundPermittedUses = repoMunicipalTrustFundPermittedUses;
    }

    public async Task<int> Handle(SaveMunicipalTrustFundPermittedUsesCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var municipalTrustFundPermittedUses = mapper.Map<SaveMunicipalTrustFundPermittedUsesCommand, FloodMunicipalTrustFundPermittedUsesEntity>(request);
        int id = await repoMunicipalTrustFundPermittedUses.SaveAsync(municipalTrustFundPermittedUses);

        return id;
    }
}
