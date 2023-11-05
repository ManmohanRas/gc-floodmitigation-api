namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftCostDetailsQuery : IRequest<GetSoftCostDetailsQueryViewModel>
{   
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
