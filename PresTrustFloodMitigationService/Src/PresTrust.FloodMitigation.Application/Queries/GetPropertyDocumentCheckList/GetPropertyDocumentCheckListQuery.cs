namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentChecklistQuery : IRequest<IEnumerable<PropertyDocumentChecklistSectionViewModel>>
    {
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public string UserId { get; set; }
    }
}
