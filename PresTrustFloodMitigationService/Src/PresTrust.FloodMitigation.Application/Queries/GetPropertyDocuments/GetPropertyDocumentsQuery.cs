namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentsQuery : IRequest<IEnumerable<PropertyDocumentTypeViewModel>>
    {
        public int ApplicationId { get; set; }
        public string Pamspin { get; set; }
        public string SectionName { get; set; }
    }
}
