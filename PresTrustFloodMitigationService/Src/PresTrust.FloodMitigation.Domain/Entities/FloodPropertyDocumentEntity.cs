namespace PresTrust.FloodMitigation.Domain.Entities
{
    public class FloodPropertyDocumentEntity
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string? PamsPin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int DocumentTypeId { get; set; }
        public PropertyDocumentTypeEnum DocumentType
        {
            get
            {
                return (PropertyDocumentTypeEnum)DocumentTypeId;
            }
            set
            {
                this.DocumentTypeId = (int)value;
            }
        }

        public bool ShowCommitte { get; set; }
        public bool UseInReport { get; set; }

        public int SectionId { get; set; }

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

        public bool HardCopy { get; set; }
        public bool Approved { get; set; }
        public string? ReviewComment { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
