namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelSurveyCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string? Surveyor { get; set; }
    public DateTime? SurveyDate { get; set; }
    public DateTime LastRevision { get; set; }
    public DateTime DateCorrected { get; set; }
}
