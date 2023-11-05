namespace PresTrust.FloodMitigation.Application.Commands
{
    public class DeletePropertyDocumentCommand : IRequest<bool>
    {
        public int ApplicationId { get; set; }
        public int Id { get; set; }
        public string PamsPin { get; set; }
    }
}
