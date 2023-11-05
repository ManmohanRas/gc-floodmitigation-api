namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelTrackingQuery : IRequest<GetParcelTrackingQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }

}
