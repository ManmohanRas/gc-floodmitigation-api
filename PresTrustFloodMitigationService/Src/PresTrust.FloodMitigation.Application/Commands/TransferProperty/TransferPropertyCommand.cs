namespace PresTrust.FloodMitigation.Application.Commands;

public class TransferPropertyCommand : IRequest<TransferPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string PamsPin { get; set; }
}
