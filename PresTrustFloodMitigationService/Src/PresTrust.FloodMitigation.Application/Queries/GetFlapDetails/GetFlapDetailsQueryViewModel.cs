namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQueryViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public bool FlapApproved { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? LastRevisedDate { get; set; }
    public DateTime? FlapMailToGrantee { get; set; }
    public List<FloodFlapCommentEntity>? FlapComments { get; set; }

}
