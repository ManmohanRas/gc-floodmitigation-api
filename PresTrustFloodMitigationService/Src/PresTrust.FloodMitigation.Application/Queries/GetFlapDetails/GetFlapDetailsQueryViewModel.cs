namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQueryViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public bool FlapApproved { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? LastRevisedDate { get; set; }
    public DateTime? FlapMailToGrantee { get; set; }
    public IEnumerable<FlapCommentViewModel>? FlapComments { get; set; }
    public List<FlapDocumentTypeViewModel> DocumentsTree { get; set; } = new List<FlapDocumentTypeViewModel>();
}

public class FlapCommentViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string Comment { get; set; }
    public string? RowStatus { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}

public class FlapDocumentTypeViewModel
{
    public string DocumentType { get; set; }
    public List<FlapDocumentViewModel> Documents { get; set; }

}

public class FlapDocumentViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string DocumentType { get; set; }
}
