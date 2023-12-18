using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using System.Text.RegularExpressions;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQueryHandler : IRequestHandler<GetFlapDetailsQuery, GetFlapDetailsQueryViewModel>
{
    private IMapper mapper;
    private IFlapModuleRepository repoFlap;

    public GetFlapDetailsQueryHandler(
        IMapper mapper,
        IFlapModuleRepository repoFlap
        )
    {
        this.mapper = mapper;
        this.repoFlap = repoFlap;
    }
    public async Task<GetFlapDetailsQueryViewModel> Handle(GetFlapDetailsQuery request, CancellationToken cancellationToken)
    {
        //get flap details
        var reqFlap = await repoFlap.GetFlapAsync(request.AgencyId);
        var flapComments = await repoFlap.GetFlapCommentsAsync(request.AgencyId);

        var flap = mapper.Map<FloodFlapEntity, GetFlapDetailsQueryViewModel>(reqFlap);
        flap.FlapComments = mapper.Map<IEnumerable<FloodFlapCommentEntity>, IEnumerable<FlapCommentViewModel>>(flapComments);

        flap.DocumentsTree = await GetDocuments(request.AgencyId);

        return flap;

    }

    private async Task<List<FlapDocumentTypeViewModel>> GetDocuments(int agencyId)
    {
        var documents = await repoFlap.GetFlapDocumentsAsync(agencyId);

        IEnumerable<IGrouping<string, FloodFlapDocumentEntity>> query =
            documents.GroupBy(doc => doc.DocumentType.ToString());

        List<FlapDocumentTypeViewModel> documentsTree = new List<FlapDocumentTypeViewModel>();
        foreach (IGrouping<string, FloodFlapDocumentEntity> docGroup in query)
        {
            List<FlapDocumentViewModel> docs = new List<FlapDocumentViewModel>();
            foreach (var doc in docGroup)
            {
                var vm = mapper.Map<FloodFlapDocumentEntity, FlapDocumentViewModel>(doc);
                if (doc.Id > 0) docs.Add(vm);
            }
            var vmDocType = new FlapDocumentTypeViewModel()
            {
                DocumentType = docGroup.Key,
                Documents = docs
            };
            documentsTree.Add(vmDocType);
        }
           
        
        return documentsTree;
    }
}
