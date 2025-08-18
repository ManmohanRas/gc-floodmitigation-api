namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTechDetailsQuery : IRequest<GetTechDetailsQueryViewModel> 
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }
}
