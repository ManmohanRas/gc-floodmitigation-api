namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodFundingSourceViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int FundingSourceTypeId { get; set; }
    public string? Title { get;set; }
    public decimal Amount { get; set; }
    public DateTime? AwardDate { get; set; }
    public string RowStatus { get; set; } = "";

}
