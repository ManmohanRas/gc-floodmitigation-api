namespace PresTrust.FloodMitigation.Application.Services.EmailApi
{
    public class EmailRequest
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public string Attachments { get; set; }
    }
}
