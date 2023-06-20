using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Domain.Entities;

public class FlmitigFeedbackEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int SectionId { get; set; }
    public string Feedback { get; set; }
    public bool RequestForCorrection { get; set; }
    public string CorrectionStatus { get; set; }
    public bool MarkRead { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
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
}
