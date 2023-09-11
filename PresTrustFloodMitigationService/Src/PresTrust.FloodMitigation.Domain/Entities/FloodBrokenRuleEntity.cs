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
}
