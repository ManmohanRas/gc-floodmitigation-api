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
    public int PrevStatusId { get; set; }
    public bool CreatedByProgramAdmin { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public bool IsActive { get; set; }
    public int? NoOfHomes { get; set; }
    public int? NoOfContiguousHomes { get; set; }
    public string AgencyJSON { get; set; }
    public string CommentsJSON { get; set; }
    public string FeedbacksJSON { get; set; }

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
    public PropertyStatusEnum PrevStatus
    {
        get
        {
            return (PropertyStatusEnum)PrevStatusId;
        }
        set
        {
            this.PrevStatusId = (int)value;
        }
    }

    public FloodAgencyEntity Agency {
        get
        {
            return this.AgencyJSON == null ? new () : JsonSerializer.Deserialize<FloodAgencyEntity>(this.AgencyJSON);
        }
    }

    public IEnumerable<FloodApplicationCommentEntity> Comments
    {
        get
        {
            return this.CommentsJSON == null ? new List<FloodApplicationCommentEntity>() : JsonSerializer.Deserialize<IEnumerable<FloodApplicationCommentEntity>>(this.CommentsJSON);
        }
    }

    public IEnumerable<FloodApplicationFeedbackEntity> Feedbacks
    {
        get
        {
            return this.FeedbacksJSON == null ? new List<FloodApplicationFeedbackEntity>() : JsonSerializer.Deserialize<IEnumerable<FloodApplicationFeedbackEntity>>(this.FeedbacksJSON);
        }
    }
}


