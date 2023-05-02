namespace PresTrust.FloodMitigation.Application.Services.EmailApi
{
    public class EmailResponse
    {
        public string Status { get; set; }
        public DateTime MessageDate { get; set; }
        public EmailMessage Message { get; set; }
    }
}
