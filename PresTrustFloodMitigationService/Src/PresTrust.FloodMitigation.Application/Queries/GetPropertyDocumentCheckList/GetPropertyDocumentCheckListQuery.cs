namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentCheckListQuery : IRequest<IEnumerable<PropertyDocumentCheckListSectionViewModel>>
    {
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
    }
}
