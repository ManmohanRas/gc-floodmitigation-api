namespace PresTrust.FloodMitigation.Application.Commands
{
    public class SavePropertyDocumentDetailsCommand : IRequest<SavePropertyDocumentDetailsCommandViewModel>
    {
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public string? DocumentType { get; set; }
        public string? UserId { get; set; }
    }
}
