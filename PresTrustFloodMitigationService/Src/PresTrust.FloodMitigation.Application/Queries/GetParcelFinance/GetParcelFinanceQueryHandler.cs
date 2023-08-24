using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelFinanceQueryHandler : BaseHandler, IRequestHandler<GetParcelFinanceQuery, GetParcelFinanceQueryViewModel>
{
    private IMapper mapper;
    private IApplicationRepository repoApplication;
    private IParcelFinanceRepository repoParcelFinance;
    
    public GetParcelFinanceQueryHandler(
        IMapper mapper
       ,IApplicationRepository repoApplication
       ,IParcelFinanceRepository repoParcelFinance
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoParcelFinance = repoParcelFinance;
    }
    public async Task<GetParcelFinanceQueryViewModel> Handle(GetParcelFinanceQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get parcel finance
        var parcelFinance = await this.repoParcelFinance.GetParceFinanceAsync(request.ApplicationId, request.PamsPin);
        parcelFinance = parcelFinance ?? new FloodParcelFinanceEntity()
        {
            ApplicationId = application.Id
        };

        //to do soft cost sums and ROF

        var result = mapper.Map<FloodParcelFinanceEntity, GetParcelFinanceQueryViewModel>(parcelFinance);

        return result;
    }
}
