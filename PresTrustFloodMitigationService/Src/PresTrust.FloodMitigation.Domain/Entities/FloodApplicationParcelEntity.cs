namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationParcelEntity
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int StatusId { get; set; }
    public int PrevStatusId { get; set; }
    public bool IsLocked { get; set; }
    public bool? IsSubmitted { get; set; }
    public bool? IsApproved { get; set; } 
    public string? LastUpdatedBy { get; set; }
    public PropertyStatusEnum Status
    {
        get
        {
            return (PropertyStatusEnum)StatusId;
        }
        set
        {
            this.StatusId = (int)value;
        }
    }
}


