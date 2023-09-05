namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftcostDetailsQuery : IRequest<GetSoftcostDetailsQueryViewModel>
{   
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
}
