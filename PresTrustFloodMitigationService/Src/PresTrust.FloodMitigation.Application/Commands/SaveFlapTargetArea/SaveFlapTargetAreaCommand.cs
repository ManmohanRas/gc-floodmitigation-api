namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapTargetAreaCommand: IRequest<int>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string TargetArea { get; set; }
    public DateTime? CreatedDate { get; set; }
    public List<int>? ParcelIds { get; set; }
}
