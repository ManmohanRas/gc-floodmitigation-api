namespace PresTrust.FloodMitigation.Application.CommonViewModels
{
    public class PropertyDocumentViewModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public bool ShowCommitte { get; set; }
        public bool UseInReport { get; set; }
        public string Section { get; set; }
        public bool HardCopy { get; set; }
        public bool Approved { get; set; }
        public string ReviewComment { get; set; }
        public string RowStatus { get; set; }
    }
}
