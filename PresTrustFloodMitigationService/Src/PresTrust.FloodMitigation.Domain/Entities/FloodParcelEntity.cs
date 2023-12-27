namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QCode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string StreetNo { get; set; }
    public string StreetAddress { get; set; }
    public string PropertyAddress { get; set; }
    public decimal Acreage { get; set; }
    public string LandOwner { get; set; }
    public string OwnersAddress1 { get; set; }
    public string OwnersAddress2 { get; set; }
    public string OwnersCity { get; set; }
    public string OwnersState { get; set; }
    public string OwnersZipcode { get; set; }
    public decimal SquareFootage { get; set; }
    public int YearOfConstruction { get; set; }
    public decimal TotalAssessedValue { get; set; }
    public decimal LandValue { get; set; }
    public decimal ImprovementValue { get; set; }
    public decimal AnnualTaxes { get; set; }
    public string TargetArea { get; set; }
    public bool IsFLAP { get; set; }
    public DateTime DateOfFLAP { get; set; }
    public bool IsElevated { get; set; }
    public int StatusId { get; set; }
    public int PrevStatusId { get; set; }
    public bool IsLocked { get; set; }
    public bool? IsSubmitted { get; set; }
    public bool? IsApproved { get; set; }
    public bool IsWaiting { get; set; }
    public bool AlreadyExists { get; set; }
    public bool IsRejected { get; set; }
    public bool IsValidPamsPin { get; set; }
    public int Priority { get; set; }
    public decimal ValueEstimate { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public decimal FinalOffer
    {
        get
        {
            return HardCostFMPAmt + SoftCostFMPAmt;
        }
    }
    public decimal ProgramMatch { get; set; }
    public int StartNo { get; set; }
    public int EndNo { get; set; }
    public int TotalNo { get; set; }
    public string CommentsJSON { get; set; }
    public string FeedbacksJSON { get; set; }
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

    public IEnumerable<FloodPropertyCommentEntity> Comments
    {
        get
        {
            return this.CommentsJSON == null ? new List<FloodPropertyCommentEntity>() : JsonSerializer.Deserialize<IEnumerable<FloodPropertyCommentEntity>>(this.CommentsJSON);
        }
    }

    public IEnumerable<FloodPropertyFeedbackEntity> Feedbacks
    {
        get
        {
            return this.FeedbacksJSON == null ? new List<FloodPropertyFeedbackEntity>() : JsonSerializer.Deserialize<IEnumerable<FloodPropertyFeedbackEntity>>(this.FeedbacksJSON);
        }
    }
}
