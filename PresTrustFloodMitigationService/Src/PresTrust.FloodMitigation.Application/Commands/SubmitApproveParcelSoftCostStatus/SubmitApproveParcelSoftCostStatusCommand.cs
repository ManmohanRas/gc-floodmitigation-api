namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApproveParcelSoftCostStatusCommand: IRequest<bool>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public bool? IsSubmitted { get; set; }
    public bool? IsApproved { get; set; }
}
