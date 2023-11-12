using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using PresTrust.FloodMitigation.Application.CommonViewModels;
using PresTrust.FloodMitigation.Domain.Configurations;
using PresTrust.FloodMitigation.Domain.Entities;
using PresTrust.FloodMitigation.Domain.Enums;
using PresTrust.FloodMitigation.Domain.Utils;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Commands
{
    /// <summary>
    /// This class handles the command to update data and build response
    /// </summary>
    public class SaveApplicationDocumentChecklistCommandHandler : BaseHandler, IRequestHandler<SaveApplicationDocumentChecklistCommand, Unit>
    {
        private IMapper mapper;
        private readonly IPresTrustUserContext userContext;
        private readonly SystemParameterConfiguration systemParamOptions;
        private readonly IApplicationRepository repoApplication;
        private readonly IApplicationDocumentRepository repoDocument;
        //private readonly ISiteRepository repoSite;
        // private readonly IBrokenRuleRepository repoBrokenRules;


        public SaveApplicationDocumentChecklistCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            IApplicationDocumentRepository repoDocument
            //ISiteRepository repoSite,
            // IBrokenRuleRepository repoBrokenRules
        ) : base(repoApplication: repoApplication)
        {
            this.mapper = mapper;
            this.userContext = userContext;
            this.systemParamOptions = systemParamOptions.Value;
            this.repoApplication = repoApplication;
            this.repoDocument = repoDocument;
            //this.repoSite = repoSite;
            // this.repoBrokenRules = repoBrokenRules;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SaveApplicationDocumentChecklistCommand request, CancellationToken cancellationToken)
        {
            // get application details
            var application = await GetIfApplicationExists(request.ApplicationId);

            // consider only add/updated records
            var viewmodelDocuments = request.Documents.Where(doc => string.Compare(doc.RowStatus, "U", ignoreCase: true) == 0).ToList();

            // map command object to the HistDocumentEntity
            var entityDocuments = mapper.Map<IEnumerable<ApplicationDocumentViewModel>, IEnumerable<FloodApplicationDocumentEntity>>(viewmodelDocuments);

            // returns broken rules  
            // var brokenRules = ReturnBrokenRulesIfAny(application, request);


            // save application documents, property documents (review/checklist items)
            using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
            {
                foreach (var doc in entityDocuments)
                {
                    await repoDocument.SaveApplicationDocumentChecklistAsync(doc);
                }
                //await repoBrokenRules.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.ADMIN_DOCUMENT_CHECKLIST);
                //await repoBrokenRules.SaveBrokenRules(await brokenRules);

                scope.Complete();
            };

            return Unit.Value;
        }

        /// <summary>
        /// Return broken rules in case of any business rule failure
        /// </summary>
        /// <param name="application"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        //private async Task<List<BrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity application, UpdateDocumentChecklistCommand request)
        //{

        //    int sectionId = (int)ApplicationSectionEnum.ADMIN_DOCUMENT_CHECKLIST;
        //    List<BrokenRuleEntity> brokenRules = new List<BrokenRuleEntity>();
        //    // map command object to the HistDocumentEntity
        //    var documents = mapper.Map<IEnumerable<DocumentViewModel>, IEnumerable<FloodDocumentEntity>>(request.Documents);

        //    //var documents = await repoDocument.GetDocumentsAsync(request.ApplicationId, sectionId);

        //    //exclude document types from section Admin Pending, Admin Release of Funds and Admin Easements
        //    // documents = documents.Where(doc => doc.Section != ApplicationSectionEnum.ADMIN_PENDING && doc.Section != ApplicationSectionEnum.ADMIN_RELEASE_OF_FUNDS &&  doc.Section != ApplicationSectionEnum.ADMIN_EASEMENTS);
        //    // documents = documents.Where(doc => doc.Section != ApplicationSectionEnum.ADMIN_PENDING && doc.Section != ApplicationSectionEnum.ADMIN_RELEASE_OF_FUNDS &&  doc.Section != ApplicationSectionEnum.ADMIN_EASEMENTS);
        //    var unapprovedDocs = documents.Where(doc => doc.Approved == false).ToList();

        //    if (documents == null || documents.Count() == 0)
        //    {
        //        brokenRules.Add(new BrokenRuleEntity()
        //        {
        //            ApplicationId = application.Id,
        //            SectionId = sectionId,
        //            Message = "All required documents (Admin-Document-Checklist) are not yet uploaded for committee review.",
        //            IsApplicantFlow = false
        //        });
        //    }

        //    if (unapprovedDocs != null && unapprovedDocs.Count() > 0)
        //    {
        //        brokenRules.Add(new BrokenRuleEntity()
        //        {
        //            ApplicationId = application.Id,
        //            SectionId = sectionId,
        //            Message = "All required documents (Admin-Document-Checklist) are not yet approved for committee review.",
        //            IsApplicantFlow = false
        //        });
        //    }

        //    return brokenRules;
        //}

    }
}
    
