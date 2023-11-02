namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationStatusLogEntity
{
    public int ApplicationId { get; set; }
    public int StatusId { get; set; }
    public DateTime StatusDate { get; set; }
    public string Notes { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public ApplicationStatusEnum Status
    {
        get
        {
            return (ApplicationStatusEnum)StatusId;
        }
        set
        {
            this.StatusId = (int)value;
        }
    }
}


