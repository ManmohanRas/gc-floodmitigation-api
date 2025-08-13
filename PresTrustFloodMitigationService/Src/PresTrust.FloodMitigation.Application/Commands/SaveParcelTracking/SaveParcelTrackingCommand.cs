namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelTrackingCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public DateTime? ClosingDate { get; set; }
    public string? DeedBook { get; set; }
    public string? DeedPage { get; set; }
    public DateTime? DeedDate { get; set; }
    public DateTime? DemolitionDate { get; set; }
    public DateTime? SiteVisitConfirmDate { get; set; }
    public bool PublicPark { get; set; }
    public bool RainGarden { get; set; }
    public bool CommunityGarden { get; set; }
    public bool ActiveRecreation { get; set; }
    public bool NaturalHabitat { get; set; }
    public string UserId { get; set; }


}
