namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationEntity
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
    public string Title { get; set; }
    public int ApplicationTypeId { get; set; }
    public int ApplicationSubTypeId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int StatusId { get; set; }
    public bool CreatedByProgramAdmin { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public bool IsActive { get; set; }

    public ApplicationTypeEnum ApplicationType
    {
        get
        {
            return (ApplicationTypeEnum)ApplicationTypeId;
        }
        set
        {
            this.ApplicationTypeId = (int)value;
        }
    }

    public ApplicationSubTypeEnum ApplicationSubType
    {
        get
        {
            return (ApplicationSubTypeEnum)ApplicationSubTypeId;
        }
        set
        {
            this.ApplicationSubTypeId = (int)value;
        }
    }

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


