namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteContactCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
}
