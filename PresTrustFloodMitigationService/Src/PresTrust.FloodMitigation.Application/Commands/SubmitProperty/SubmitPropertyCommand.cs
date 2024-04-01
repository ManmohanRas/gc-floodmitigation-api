namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitPropertyCommand : IRequest<SubmitPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
