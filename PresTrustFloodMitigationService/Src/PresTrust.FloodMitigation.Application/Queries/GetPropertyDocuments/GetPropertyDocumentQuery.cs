namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentQuery : IRequest<IEnumerable<PropertyDocumentTypeViewModel>>
    {
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public string SectionName { get; set; }
        public string UserId { get; set; }
    }
}
