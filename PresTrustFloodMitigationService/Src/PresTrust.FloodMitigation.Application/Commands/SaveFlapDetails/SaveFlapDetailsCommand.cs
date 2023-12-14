namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDetailsCommand: IRequest<Unit>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public bool FlapApproved { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? LastRevisedDate { get; set; }
    public DateTime? FlapMailToGrantee { get; set; }
    public List<FloodFlapCommentViewModel> FlapComments { get; set; } = new List<FloodFlapCommentViewModel>();
}

public class FloodFlapCommentViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string Comment { get; set; }
    public string? RowStatus { get; set; } = "";
}
