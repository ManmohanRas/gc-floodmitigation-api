namespace PresTrust.FloodMitigation.Application.BackgroundJobs;

public interface IGrantExpirationReminder
{
    void SendEmail(string backGroundJobType, string startTime);
    Task Handle();
}
