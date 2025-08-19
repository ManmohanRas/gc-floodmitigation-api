namespace PresTrust.FloodMitigation.Application.Commands
{
    public class DeletePropertyDocumentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
