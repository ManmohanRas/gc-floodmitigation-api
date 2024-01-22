namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalCommentsQueryViewModel
{
    public int Id { get; set; } = 0;
    public int AgencyId { get; set; } = 0;
    public string Comment { get; set; } = "";
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
