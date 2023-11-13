namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationDocumentEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int DocumentTypeId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public bool UseInReport { get; set; }
    public bool HardCopy { get; set; }
    public bool Approved { get; set; }
    public string ReviewComment { get; set; }
    public int SectionId { get; set; }
    public bool ShowCommitte { get; set; }
    public int? OtherFundingSourceId { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public ApplicationDocumentTypeEnum DocumentType
    {
        get
        {
            return (ApplicationDocumentTypeEnum)DocumentTypeId;
        }
        set
        {
            this.DocumentTypeId = (int)value;
        }
    }
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
