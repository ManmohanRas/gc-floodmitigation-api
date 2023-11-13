namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationFundingAgencyCommandHandler :BaseHandler, IRequestHandler<SaveApplicationFundingAgencyCommand, int>
{
    private readonly IMapper mapper;
    private readonly IApplicationFundingAgencyRepository repoFundingAgency;
    private readonly IApplicationDocumentRepository repoDocument;



    public SaveApplicationFundingAgencyCommandHandler(
        IMapper mapper,
        IApplicationFundingAgencyRepository repoFundingAgency,
        IApplicationDocumentRepository repoDocument
        )
    {
        this.mapper = mapper;
        this.repoFundingAgency  = repoFundingAgency;
        this.repoDocument = repoDocument;
    }
    public async Task<int> Handle(SaveApplicationFundingAgencyCommand request, CancellationToken cancellationToken)
    {
        var fundingAgency = mapper.Map<SaveApplicationFundingAgencyCommand, FloodApplicationFundingAgencyEntity>(request);
        fundingAgency = await repoFundingAgency.SaveAsync(fundingAgency);

        var documents = await GetDocuments(request.ApplicationId);
        foreach( var doc in documents) 
        {
            doc.OtherFundingSourceId = fundingAgency.Id;
            await repoDocument.SaveApplicationDocumentChecklistAsync(doc);
        }

        return fundingAgency.Id;
    }
    private async Task<List<FloodApplicationDocumentEntity>> GetDocuments(int applicationId)
    {
        var documents = await repoDocument.GetApplicationDocumentsAsync(applicationId, (int)ApplicationSectionEnum.OVERVIEW);
        documents = documents.Where(o => o.DocumentType == ApplicationDocumentTypeEnum.OTHER_FUNDING_AGENCY && !(o.OtherFundingSourceId > 0)).ToList();
        return documents;
    }
}
