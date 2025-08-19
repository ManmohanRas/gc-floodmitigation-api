namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationAdminDetailsQuery :IRequest<GetApplicationAdminDetailsQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
