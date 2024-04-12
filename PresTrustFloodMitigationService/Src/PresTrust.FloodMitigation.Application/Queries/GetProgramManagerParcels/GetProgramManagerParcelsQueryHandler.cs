namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramManagerParcelsQueryHandler : BaseHandler, IRequestHandler<GetProgramManagerParcelsQuery, GetProgramManagerParcelsQueryViewModel>
{

    private IMapper mapper;
    private readonly IParcelRepository repoParcel;

    public GetProgramManagerParcelsQueryHandler(
        IMapper mapper,
        IParcelRepository repoParcel
        ) : base()
    {
        this.mapper = mapper;
        this.repoParcel = repoParcel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GetProgramManagerParcelsQueryViewModel> Handle(GetProgramManagerParcelsQuery request, CancellationToken cancellationToken)
    {
        var properties = await this.repoParcel.GetProgramManagerParcelsAsync(request.PageNumber, request.PageRows, request.SearchBlockText, request.SearchLotText, request.SearchAddressText, request.selectedAgency);
        var result = mapper.Map<FloodProgramManagerParcelsEntity, GetProgramManagerParcelsQueryViewModel>(properties);
        result.Parcels = result.Parcels.OrderBy(o => o.PamsPin).ToList();
        return result;
    }
}
