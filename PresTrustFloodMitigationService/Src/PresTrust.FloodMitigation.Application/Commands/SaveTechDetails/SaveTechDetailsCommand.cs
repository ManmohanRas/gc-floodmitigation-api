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
    public int? Claim10Years { get; set; }
    public decimal? TotalOfClaims { get; set; }
    public string? BenefitCostRatio { get; set; }
    public string? FEMACommunityId { get; set; }
    public DateTime? FirmEffectiveDate { get; set; }
    public string FirmPanel { get; set; }
    public string FirmPanelFinal { get; set; }
    public string FloodZoneDesignation { get; set; }
    public string BaseFloodElevation { get; set; }
    public string BaseFloodElevationFinal { get; set; }
    public string RiverId { get; set; }
    public string RiverIdFinal { get; set; }
    public DateTime? FisEffectiveDate { get; set; }
    public string FloodProfile { get; set; }
    public string FloodProfileFinal { get; set; }
    public string FloodSource { get; set; }
    public string FirstFloodElevation { get; set; }
    public string FirstFloodElevationFinal { get; set; }
    public string StreambedElevation { get; set; }
    public string StreambedElevationFinal { get; set; }
    public string ElevationBeforeMitigation { get; set; }
    public string ElevationBeforeMitigationFinal { get; set; }
    public string FloodType { get; set; }
    public int? TenPercent { get; set; }
    public int? TwoPercent { get; set; }
    public int? OnePercent { get; set; }
    public int? PointOnePercent { get; set; }
}
