namespace PresTrust.FloodMitigation.Domain.Entities;

public class ErrorDetailsEntity
{
    public string Title { get; set; } = "";
    public string[] Errors { get; set; } = new string[] { };
    public string DeveloperMessage { get; set; } = string.Empty;
}
