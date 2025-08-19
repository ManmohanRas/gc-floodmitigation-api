namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationDetailsQuery : IRequest<GetApplicationDetailsQueryViewModel>
{
        public int ApplicationId { get; set; }
        public string UserId { get; set; }
}
