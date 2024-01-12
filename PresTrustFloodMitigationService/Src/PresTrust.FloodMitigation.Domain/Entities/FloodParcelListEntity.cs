namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelListEntity
{
    public string PropertyAddress { get; set; }
    public string Municipality { get; set; }
    public string ProjectArea { get; set; }
    public int ApplicationId { get; set; }
    public string ApplicationType { get; set; }
    public string SubProgramType { get; set; }
    public decimal FinalOffer { get; set; }
    public decimal ProgramMatch { get; set; }
    public string PropertyStatus { get; set; }
}
