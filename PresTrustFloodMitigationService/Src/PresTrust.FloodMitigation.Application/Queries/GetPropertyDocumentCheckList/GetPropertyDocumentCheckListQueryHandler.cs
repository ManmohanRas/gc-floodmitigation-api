using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentCheckListQueryHandler : BaseHandler, IRequestHandler<GetPropertyDocumentCheckListQuery, IEnumerable<PropertyDocumentCheckListSectionViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IApplicationRepository repoApplication;
        private readonly IPropertyDocumentRepository repoDocuments;
       

        public GetPropertyDocumentCheckListQueryHandler
        (
            IMapper mapper,
            IApplicationRepository repoApplication,
            IPropertyDocumentRepository repoDocuments
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
        public async Task<IEnumerable<PropertyDocumentCheckListSectionViewModel>> Handle(GetPropertyDocumentCheckListQuery request, CancellationToken cancellationToken)
        {
            // get documents 
            var documents = await repoDocuments.GetPropertyDocumentCheckListAsync(request.ApplicationId, request.PamsPin);

            // build checklist view model
            var docBuilder = new PropertyDocumentTreeBuilder(documents, buildPropChecklist: true);
            return docBuilder.documentCheckListItems;
        }
    }
}
