namespace PresTrust.FloodMitigation.Application.Commands
{
    public class SavePropertyDocumentChecklistCommand : IRequest<Unit>
    {
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public IEnumerable<PropertyDocumentViewModel> Documents { get; set; }
    }
}
