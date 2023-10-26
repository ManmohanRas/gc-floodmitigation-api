namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationDetailsCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
}
