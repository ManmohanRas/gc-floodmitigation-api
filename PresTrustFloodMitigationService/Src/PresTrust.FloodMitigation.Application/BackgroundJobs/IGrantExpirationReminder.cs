namespace PresTrust.FloodMitigation.Application.BackgroundJobs;

public interface IGrantExpirationReminder
{
    Task Handle();
}
