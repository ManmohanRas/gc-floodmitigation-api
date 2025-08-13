using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyAdminDetailsCommandHandler : BaseHandler, IRequestHandler<SavePropertyAdminDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private IPropertyAdminDetailsRepository repoPropertyDetails;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IParcelRepository repoParcel;
    private readonly IApplicationParcelRepository repoAppParcel;
    private readonly IPropertyDocumentRepository repoPropertyDocument;
    private readonly IParcelPropertyRepository repoParcelProperty;

    public SavePropertyAdminDetailsCommandHandler
    (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       IPropertyAdminDetailsRepository repoPropertyDetails,
       IPropertyBrokenRuleRepository repoBrokenRules,
       IParcelRepository repoParcel,
       IApplicationParcelRepository repoAppParcel,
       IPropertyDocumentRepository repoPropertyDocument,
       IParcelPropertyRepository repoParcelProperty
    ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)

    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoPropertyDetails = repoPropertyDetails;
        this.repoBrokenRules = repoBrokenRules;
        this.repoAppParcel = repoAppParcel;
        this.repoPropertyDocument = repoPropertyDocument;
        this.repoParcelProperty = repoParcelProperty;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SavePropertyAdminDetailsCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);
        

        // map command object to the FloodPropertyDetailsEntity
        var reqPropDetails = mapper.Map<SavePropertyAdminDetailsCommand, FloodPropertyAdminDetailsEntity>(request);
        var reqSoftCostDetails = mapper.Map<SavePropertyAdminDetailsCommand, FloodParcelSoftCostEntity>(request);

        // Check Broken Rules
        var brokenRules = await ReturnBrokenRulesIfAny(application, property, reqPropDetails, reqSoftCostDetails);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.ADMIN_DETAILS, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);
            reqPropDetails = await repoPropertyDetails.SaveAsync(reqPropDetails);
            reqPropDetails.LastUpdatedBy = userContext.Email;
            scope.Complete();
        }
        return reqPropDetails.Id;
    }
    private async Task<List<FloodPropertyBrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodPropertyAdminDetailsEntity reqPropDetails, FloodParcelSoftCostEntity reqSoftCostDetails)
    {
        int sectionId = (int)PropertySectionEnum.ADMIN_DETAILS;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        var reqParcelProperty = await repoParcelProperty.GetAsync(applcation.Id, property.PamsPin);

        var documents = await repoPropertyDocument.GetPropertyDocumentsAsync(applcation.Id, property.PamsPin, sectionId);

        FloodPropertyDocumentEntity? docCongratulationLetterToHomeOwner = default;
        FloodPropertyDocumentEntity? docFmcFinalApprovadResolution = default;
        FloodPropertyDocumentEntity? docBccFinalApprovadResolution = default;
        FloodPropertyDocumentEntity? docGrantAgreement = default;
        FloodPropertyDocumentEntity? docFmcSoftcostReimbApprovalRes = default;
        FloodPropertyDocumentEntity? docBccSoftReimbApprovalRes = default;

        if (documents != null && documents.Count() > 0)
        {
            docCongratulationLetterToHomeOwner = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.CONGRATULATION_LETTER_HOMEOWNER).FirstOrDefault();
            docFmcFinalApprovadResolution = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.FMC_FINAL_APPROVAL).FirstOrDefault();
            docBccFinalApprovadResolution = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.BCC_FINAL_APPROVAL).FirstOrDefault();
            docGrantAgreement = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.GRANT_AGREEMENT).FirstOrDefault();
            docFmcSoftcostReimbApprovalRes = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.FMC_SOFTCOST).FirstOrDefault();
            docBccSoftReimbApprovalRes = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.BCC_SOFTCOST).FirstOrDefault();
        }


        if (property.Status == PropertyStatusEnum.IN_REVIEW)
        {
            if (applcation.Status == ApplicationStatusEnum.IN_REVIEW)
            {
                if (reqPropDetails?.DoesHomeOwnerHaveNFIPInsurance == null)
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "DoesHomeOwnerHaveNFIPInsurance required field on AdminDetails tab has not been filled.",
                        IsPropertyFlow = false
                    });
                if (reqPropDetails?.IsDEPInvolved == null)
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "IsDEPInvolved required field on AdminDetails tab has not been filled.",
                        IsPropertyFlow = false
                    });
                if (reqPropDetails?.IsPARRequestedbyFunder == null)
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "IsPARRequestedbyFunder required field on AdminDetails tab has not been filled.",
                        IsPropertyFlow = false
                    });
            }
        }

        if (applcation.ApplicationSubType != ApplicationSubTypeEnum.FASTTRACK)
        {
            if (applcation.Status == ApplicationStatusEnum.IN_REVIEW)
            {
                if (property.Status == PropertyStatusEnum.IN_REVIEW)
                {
                    if (docCongratulationLetterToHomeOwner == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Congratulation Letter To HomeOwner required Upload on AdminDetails tab have not been Uploaded.",
                            IsPropertyFlow = false
                        });
                }
            }
        }

        if (property.Status == PropertyStatusEnum.PENDING)
        {
            if (reqPropDetails?.FmcFinalApprovalDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Fmc Final Approval Date required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqPropDetails?.FmcFinalNumber == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Fmc Final Approval Number required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });
            if (reqPropDetails?.BccFinalApprovalDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Bcc Final Approval Date required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqPropDetails?.BccFinalNumber == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Bcc Final Approval Number required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqPropDetails?.MunicipalPurchaseDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Municipal Ordinance Purchase Date required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqPropDetails?.MunicipalPurchaseNumber == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Municipal Ordinance Purchase Number required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });
            if (docFmcFinalApprovadResolution == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "FMC Final Approval Resolution required Upload on AdminDetails tab have not been Uploaded.",
                    IsPropertyFlow = false
                });

            if (docBccFinalApprovadResolution == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Bcc Final Approval Resolution required Upload on AdminDetails tab have not been Uploaded.",
                    IsPropertyFlow = false
                });

            if (docGrantAgreement == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Grant Agreement required Upload on AdminDetails tab have not been Uploaded.",
                    IsPropertyFlow = false
                });
            if (applcation.ApplicationSubType == ApplicationSubTypeEnum.FASTTRACK)
            {
                if (docFmcSoftcostReimbApprovalRes == null)
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Fmc Softcost Reimbrusment Approval Resolution required Upload on AdminDetails tab have not been Uploaded.",
                        IsPropertyFlow = false
                    });
                if (docBccSoftReimbApprovalRes == null)
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Bcc SoftCost Reimbrusment Approval Resolution required Upload on AdminDetails tab have not been Uploaded.",
                        IsPropertyFlow = false
                    });
            }
        }
        if (property.Status == PropertyStatusEnum.APPROVED)
        {
            if (reqPropDetails.GrantAgreementDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Grant Agreement Date required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });
            if (reqPropDetails.GrantAgreementExpirationDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Grant Agreement Expiration Date required field on AdminDetails tab have not been filled.",
                    IsPropertyFlow = false
                });
        }

        if (property.Status == PropertyStatusEnum.PRESERVED)
        {
            if (applcation.ApplicationSubType == ApplicationSubTypeEnum.FASTTRACK)
            {
            
                if (reqParcelProperty.NeedSoftCost == true)
                {
                    if (reqPropDetails?.FmcSoftCostReimbApprovalDate == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Fmc SoftCost Reimbursment Approval Date  required field on AdminDetails tab have not been filled.",
                            IsPropertyFlow = false
                        });

                    if (reqPropDetails?.FmcSoftCostReimbApprovalNumber == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Fmc SoftCost Reimbursment Approval Number  required field on AdminDetails tab have not been filled.",
                            IsPropertyFlow = false
                        });
              
                    if (reqPropDetails?.BccSoftCostReimbApprovalDate == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Bcc SoftCost Reimbursment Approval Date  required field on AdminDetails tab have not been filled.",
                            IsPropertyFlow = false
                        });

                    if (reqPropDetails?.BccSoftCostReimbApprovalNumber == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Fmc SoftCost Reimbursment Approval Number required field on AdminDetails tab have not been filled.",
                            IsPropertyFlow = false
                        });

                    if (docBccSoftReimbApprovalRes == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Bcc SoftCost Reimbrusment Approval Resolution required Upload on AdminDetails tab have not been Uploaded.",
                            IsPropertyFlow = false
                        });

                    if (docFmcSoftcostReimbApprovalRes == null)
                        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                        {
                            ApplicationId = applcation.Id,
                            PamsPin = property.PamsPin,
                            SectionId = sectionId,
                            Message = "Fmc Softcost Reimbrusment Approval Resolution required Upload on AdminDetails tab have not been Uploaded.",
                            IsPropertyFlow = false
                        });
                }
            }
        }
        return brokenRules;
    }
}


        
