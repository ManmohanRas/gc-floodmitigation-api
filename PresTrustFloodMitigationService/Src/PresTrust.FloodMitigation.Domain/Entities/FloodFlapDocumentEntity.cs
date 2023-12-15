namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodFlapDocumentEntity
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public int DocumentTypeId { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public FlapDocumentTypeEnum DocumentType
    {
        get
        {
            return (FlapDocumentTypeEnum)DocumentTypeId;
        }
        set
        {
            this.DocumentTypeId = (int)value;
        }
    }
}
