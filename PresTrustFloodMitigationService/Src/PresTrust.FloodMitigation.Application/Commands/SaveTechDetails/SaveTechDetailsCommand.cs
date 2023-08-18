namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTechDetailsCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string Pamspin { get; set; }
    public Boolean FEMASevereRepetitiveLossList { get; set; }
    public Boolean FEMARepetitiveLossList { get; set; }
    public Boolean IsthepropertywithinthePassaicRiverBasin { get; set; }
    public Boolean IsthepropertywithinFloodway { get; set; }
    public Boolean IsthepropertywithinFloodplain { get; set; }
    public string Clame10Years { get; set; }
    public string TotalOfClaims { get; set; }
    public string BenifitCostRatio { get; set; }
    public string FEMACommunityId { get; set; }
    public DateTime? FirmEffectiveDate { get; set; }
    public string FirmPanel { get; set; }
    public string FirmPanelFinal { get; set; }
    public string FloodZoneDesignation { get; set; }
    public string BaseFloodElevation { get; set; }
    public string BaseFloodElevationFinal { get; set; }
    public string RiverId { get; set; }
    public string RiverIdFinal { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public string FloodProfile { get; set; }
    public string FloodProfileFinal { get; set; }
    public string FloodSource { get; set; }
    public string FirstFloodElivation { get; set; }
    public string FirstFloodElivationFinal { get; set; }
    public string StreambedElevation { get; set; }
    public string StreambedElevationFinal { get; set; }
    public string ElevationBeforeMitigation { get; set; }
    public string ElevationBeforeMitigationFinal { get; set; }
    public string FloodType { get; set; }
    public string TenPercent { get; set; }
    public string TwoPercent { get; set; }
    public string OnePercent { get; set; }
    public string PointonePercent { get; set; }
}
