namespace PresTrust.FloodMitigation.Application.Queries;
public class GetPropertyFeedbacksQueryViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public int PamsPin { get; set; }
    public string? Section { get; set; }
    public string Feedback { get; set; }
    public bool RequestForCorrection { get; set; } = false;
    public string CorrectionStatus { get; set; } = "";
    public bool MarkRead { get; set; } = false;
    public string LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
}
