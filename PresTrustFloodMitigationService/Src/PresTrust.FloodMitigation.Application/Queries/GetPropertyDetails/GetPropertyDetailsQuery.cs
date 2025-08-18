namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyDetailsQuery : IRequest<GetPropertyDetailsQueryViewModel>
{
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public string UserId { get; set; }
}
