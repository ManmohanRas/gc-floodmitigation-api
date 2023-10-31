namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IParcelSurveyRepository
{
    Task<FloodParcelSurveyEntity> GetSurveyAsync(int applicationId, string pamsPin);
    Task<FloodParcelSurveyEntity> SaveAsync(FloodParcelSurveyEntity parcelSurvey);
}
