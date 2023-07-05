using System.Text.Json;

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

    public ApplicationStatusEnum PrevStatus
    {
        get
        {
            return (ApplicationStatusEnum)PrevStatusId;
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

    public IEnumerable<FloodCommentEntity> Comments
    {
        get
        {
            return this.CommentsJSON == null ? new List<FloodCommentEntity>() : JsonSerializer.Deserialize<IEnumerable<FloodCommentEntity>>(this.CommentsJSON);
        }
    }

    public IEnumerable<FloodFeedbackEntity> Feedbacks
    {
        get
        {
            return this.FeedbacksJSON == null ? new List<FloodFeedbackEntity>() : JsonSerializer.Deserialize<IEnumerable<FloodFeedbackEntity>>(this.FeedbacksJSON);
        }
    }
}


