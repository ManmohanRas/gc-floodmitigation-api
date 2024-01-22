namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalCommentCommandViewModel
{
    public int Id { get; set; } = 0;
    public int AgencyId { get; set; } = 0;
    public string Comment { get; set; } = "";
}
