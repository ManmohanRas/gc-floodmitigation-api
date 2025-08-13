namespace PresTrust.FloodMitigation.Application.Commands;

public class TransferPropertyCommand : IRequest<TransferPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int TransferApplicationId { get; set; }
    public string UserId { get; set; }
}
