namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodProgramManagerParcelsEntity
{
    public int StartNo { get; set; } = 0;
    public int EndNo { get; set; } = 0;
    public int TotalNo { get; set; } = 0;
    public List<FloodParcelEntity> Parcels { get; set; } = new List<FloodParcelEntity>();
}
