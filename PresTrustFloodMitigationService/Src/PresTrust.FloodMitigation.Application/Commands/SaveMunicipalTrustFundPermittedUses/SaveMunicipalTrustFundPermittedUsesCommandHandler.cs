namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalTrustFundPermittedUsesCommandHandler:IRequestHandler<SaveMunicipalTrustFundPermittedUsesCommand, int>
{
    private readonly IMapper mapper;
    private readonly IMunicipalTrustFundPermittedUsesRepository repoMunicipalTrustFundPermittedUses;

    public SaveMunicipalTrustFundPermittedUsesCommandHandler(
            IMapper mapper,
            IMunicipalTrustFundPermittedUsesRepository repoMunicipalTrustFundPermittedUses
        )
    {
        this.mapper = mapper;
        this.repoMunicipalTrustFundPermittedUses = repoMunicipalTrustFundPermittedUses;
    }

    public async Task<int> Handle(SaveMunicipalTrustFundPermittedUsesCommand request, CancellationToken cancellationToken)
    {
        var municipalTrustFundPermittedUses = mapper.Map<SaveMunicipalTrustFundPermittedUsesCommand, FloodMunicipalTrustFundPermittedUsesEntity>(request);
        int id = await repoMunicipalTrustFundPermittedUses.SaveAsync(municipalTrustFundPermittedUses);

        return id;
    }
}
