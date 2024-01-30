namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodFundingSourceEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int FundingSourceTypeId { get; set; }
    public string? Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime? AwardDate { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public FundingSourceTypeEnum FundingSourceType
    {
        get 
        {
            return (FundingSourceTypeEnum)FundingSourceTypeId;
        }
        set 
        {
            this.FundingSourceTypeId = (int)value;
        }
    }
}
