namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramManagerParcelsQueryViewModel
{
    public int StartNo { get; set; }
    public int EndNo { get; set; }
    public int TotalNo { get; set; }
    public List<FloodParcelEntity> Parcels { get; set; }
}
