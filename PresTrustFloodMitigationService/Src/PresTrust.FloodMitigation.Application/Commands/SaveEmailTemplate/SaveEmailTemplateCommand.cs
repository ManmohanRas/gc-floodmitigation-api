namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveEmailTemplateCommand: IRequest<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string TemplateCode { get; set; }
    public string Description { get; set; }
}
