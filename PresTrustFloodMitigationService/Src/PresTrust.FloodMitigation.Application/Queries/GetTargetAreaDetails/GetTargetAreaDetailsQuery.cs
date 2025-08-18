namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTargetAreaDetailsQuery: IRequest<GetTargetAreaDetailsQueryViewModel>
{
    public int Id { get; set; }
    public string UserId { get; set; }
}
