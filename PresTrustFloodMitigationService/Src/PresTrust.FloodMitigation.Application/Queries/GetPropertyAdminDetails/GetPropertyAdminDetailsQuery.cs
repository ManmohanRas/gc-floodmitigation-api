namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyAdminDetailsQuery : IRequest<GetPropertyAdminDetailsQueryViewModel>
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
}
