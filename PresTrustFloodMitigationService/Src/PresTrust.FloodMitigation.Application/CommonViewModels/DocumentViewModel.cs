namespace PresTrust.FloodMitigation.Application.CommonViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int SiteId { get; set; }
        public int EasementId { get; set; }
        public int PaymentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public bool UseInReport { get; set; }
        public string Section { get; set; }
        public bool HardCopy { get; set; }
        public bool Approved { get; set; }
        public string ReviewComment { get; set; }
        public bool IsPropertyDocument { get; set; }
        public string FundingYear { get; set; }
        public string RowStatus { get; set; }
    }
}
