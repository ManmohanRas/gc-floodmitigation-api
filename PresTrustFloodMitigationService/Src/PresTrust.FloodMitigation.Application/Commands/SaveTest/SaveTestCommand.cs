namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTestCommand : IRequest<SaveTestCommandViewModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
