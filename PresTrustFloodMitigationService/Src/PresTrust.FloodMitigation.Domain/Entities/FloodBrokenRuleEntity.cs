using static PresTrust.FloodMitigation.Domain.Constants.FloodMitigationDomainConstants;

namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodBrokenRuleEntity
{
    public int ApplicationId { get; set; }
    public int SectionId { get; set; }
    public bool IsApplicantFlow { get; set; }
    public string Message { get; set; }

    public ApplicationSectionEnum Section
    {
        get
        {
            return (ApplicationSectionEnum)SectionId;
        }
        set
        {
            this.SectionId = (int)value;
        }
    }
    public string RouterLink
    {
        get
        {
            string val = string.Empty;
            switch (this.Section)
            {
                case ApplicationSectionEnum.DECLARATION_OF_INTENT:
                    val = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT;
                    break;
                case ApplicationSectionEnum.ROLES:
                    val = ApplicationRouterLinks.ROLES_EDIT;
                    break;
                case ApplicationSectionEnum.OVERVIEW:
                    val = ApplicationRouterLinks.OVERVIEW_EDIT;
                    break;
                case ApplicationSectionEnum.PROJECT_AREA:
                    val = ApplicationRouterLinks.PROJECT_AREA_EDIT;
                    break;
                case ApplicationSectionEnum.FINANCE:
                    val = ApplicationRouterLinks.FINANCE_EDIT;
                    break;
                case ApplicationSectionEnum.SIGNATORY:
                    val = ApplicationRouterLinks.SIGNATORY_EDIT;
                    break;
                case ApplicationSectionEnum.OTHER_DOCUMENTS:
                    val = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT;
                    break;
                case ApplicationSectionEnum.ADMIN_DOCUMENT_CHECKLIST:
                    val = ApplicationRouterLinks.ADMIN_DOCUMENT_CHECKLIST_EDIT;
                    break;
                case ApplicationSectionEnum.ADMIN_DETAILS:
                    val = ApplicationRouterLinks.ADMIN_DETAILS_EDIT;
                    break;
                case ApplicationSectionEnum.ADMIN_CONTACTS:
                    val = ApplicationRouterLinks.ADMIN_CONTACTS_EDIT;
                    break;
                case ApplicationSectionEnum.ADMIN_RELEASE_OF_FUNDS:
                    val = ApplicationRouterLinks.ADMIN_RELEASE_OF_FUNDS_EDIT;
                    break;
                //case ApplicationSectionEnum.CONSULTANT_COMMENT:
                //    val = "CONSULTANT_COMMENT";
                //    break;
                default:
                    break;
            }
            return val;
        }
    }

    public FloodBrokenRuleEntity()
    {
    }

    public FloodBrokenRuleEntity(int applicationId, int sectionId, string message, bool IsApplicantFlow)
    {
        this.ApplicationId = applicationId;
        this.SectionId = sectionId;
        this.Message = message;
        this.IsApplicantFlow = IsApplicantFlow;
    }
}
