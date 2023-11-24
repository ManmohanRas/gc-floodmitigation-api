using static PresTrust.FloodMitigation.Domain.Constants.FloodMitigationDomainConstants;

namespace PresTrust.FloodMitigation.Domain.Entities
{
    public class FloodPropertyBrokenRuleEntity
    {
        public int ApplicationId { get; set; }
        public int SectionId { get; set; }
        public string? PamsPin { get; set; }
        public bool IsPropertyFlow { get; set; }
        public string? Message { get; set; }

        public PropertySectionEnum Section
        {
            get
            {
                return (PropertySectionEnum)SectionId;
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
                    case PropertySectionEnum.PROPERTY:
                        val = PropertyRouterLinks.PROPERTY_EDIT;
                        break;
                    case PropertySectionEnum.TECH:
                        val = PropertyRouterLinks.TECH_EDIT;
                        break;
                    case PropertySectionEnum.SOFT_COSTS:
                        val = PropertyRouterLinks.SOFT_COSTS_EDIT;
                        break;
                    case PropertySectionEnum.FINANCE:
                        val = PropertyRouterLinks.FINANCE_EDIT;
                        break;
                    case PropertySectionEnum.OTHER_DOCUMENTS:
                        val = PropertyRouterLinks.OTHER_DOCUMENTS_EDIT;
                        break;
                    case PropertySectionEnum.ADMIN_DOCUMENT_CHECKLIST:
                        val = PropertyRouterLinks.ADMIN_DOCUMENT_CHECKLIST_EDIT;
                        break;
                    case PropertySectionEnum.ADMIN_DETAILS:
                        val = PropertyRouterLinks.ADMIN_DETAILS_EDIT;
                        break;
                    case PropertySectionEnum.ADMIN_SURVEY:
                        val = PropertyRouterLinks.ADMIN_SURVEY_EDIT;
                        break;
                    case PropertySectionEnum.ADMIN_RELEASE_OF_FUNDS:
                        val = PropertyRouterLinks.ADMIN_RELEASE_OF_FUNDS_EDIT;
                        break;
                    default:
                        break;
                }
                return val;
            }
        }

        public FloodPropertyBrokenRuleEntity() 
        {
        }

        public FloodPropertyBrokenRuleEntity(int applicationId,  int sectionId, string pamsPin, bool IsPropertyFlow, string message)
        {
            this.ApplicationId = applicationId;
            this.PamsPin = pamsPin;
            this.SectionId = sectionId;
            this.Message = message;
            this.IsPropertyFlow = IsPropertyFlow;
        }
    }
}
