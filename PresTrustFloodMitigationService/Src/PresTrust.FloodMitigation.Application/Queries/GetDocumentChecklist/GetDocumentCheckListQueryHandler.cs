

namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetDocumentCheckListQueryHandler : BaseHandler, IRequestHandler<GetDocumentCheckListQuery, IEnumerable<DocumentCheckListSectionViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IApplicationRepository repoApplication;
        //private readonly ISiteRepository repoSite;
        private readonly IApplicationDocumentRepository repoDocuments;

        public GetDocumentCheckListQueryHandler
        (
            IMapper mapper,
            IApplicationRepository repoApplication,
            IApplicationDocumentRepository repoDocuments
        ) : base(repoApplication: repoApplication)
        {
            this.mapper = mapper;
            this.repoApplication = repoApplication;
            this.repoDocuments = repoDocuments;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DocumentCheckListSectionViewModel>> Handle(GetDocumentCheckListQuery request, CancellationToken cancellationToken)
        {
            // get application details
            // var application = await GetIfApplicationExists(request.ApplicationId);

            bool hasCOEDocument = false;
            //if (application != null )
            //{
            //    if (site != null)
            //    {
            //        if (!site.IsPropertyListed && site.ExpectingCOESHPOOpinion)
            //        {
            //            hasCOEDocument = true;
            //        }
            //        else
            //        {
            //            hasCOEDocument = false;
            //        }
            //    }
            //}

            // get documents 
            var documents = await repoDocuments.GetDocumentCheckListAsync(request.ApplicationId, hasCOEDocument);

            // build checklist view model
            var docBuilder = new ApplicationDocumentTreeBuilder(documents, buildChecklist: true);
            return docBuilder.DocumentCheckListItems;
        }
    }
}
