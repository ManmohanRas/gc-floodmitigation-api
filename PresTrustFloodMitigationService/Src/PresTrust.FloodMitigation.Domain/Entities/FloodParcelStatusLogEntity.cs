namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelStatusLogEntity
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int StatusId { get; set; }
    public DateTime StatusDate { get; set; }
    public string Notes { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
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


