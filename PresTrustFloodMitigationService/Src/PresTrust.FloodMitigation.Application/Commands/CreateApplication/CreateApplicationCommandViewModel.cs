namespace PresTrust.FloodMitigation.Application.Commands;

public class CreateApplicationCommandViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int AgencyId { get; set; }
    public string ApplicationType { get; set; } = ApplicationTypeEnum.NONE.ToString();
    public string ApplicationSubType { get; set; } = ApplicationSubTypeEnum.NONE.ToString();
    public string Status { get; set; } = ApplicationStatusEnum.NONE.ToString();
}
