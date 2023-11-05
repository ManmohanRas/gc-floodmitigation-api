namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTechDetailsCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public bool FEMASevereRepetitiveLossList { get; set; }
    public bool FEMARepetitiveLossList { get; set; }
    public bool IsthepropertywithinthePassaicRiverBasin { get; set; }
    public bool IsthepropertywithinFloodway { get; set; }
    public bool IsthepropertywithinFloodplain { get; set; }
    public int Clame10Years { get; set; }
    public decimal TotalOfClaims { get; set; }
    public decimal BenifitCostRatio { get; set; }
    public int FEMACommunityId { get; set; }
    public DateTime? FirmEffectiveDate { get; set; }
    public string FirmPanel { get; set; }
    public string FirmPanelFinal { get; set; }
    public string FloodZoneDesignation { get; set; }
    public int BaseFloodElevation { get; set; }
    public int BaseFloodElevationFinal { get; set; }
    public int RiverId { get; set; }
    public int RiverIdFinal { get; set; }
    public DateTime? FisEffectiveDate { get; set; }
    public string FloodProfile { get; set; }
    public string FloodProfileFinal { get; set; }
    public string FloodSource { get; set; }
    public decimal FirstFloodElivation { get; set; }
    public decimal FirstFloodElivationFinal { get; set; }
    public decimal StreambedElevation { get; set; }
    public decimal StreambedElevationFinal { get; set; }
    public decimal ElevationBeforeMitigation { get; set; }
    public decimal ElevationBeforeMitigationFinal { get; set; }
    public string FloodType { get; set; }
    public int TenPercent { get; set; }
    public int TwoPercent { get; set; }
    public int OnePercent { get; set; }
    public int PointOnePercent { get; set; }
}
