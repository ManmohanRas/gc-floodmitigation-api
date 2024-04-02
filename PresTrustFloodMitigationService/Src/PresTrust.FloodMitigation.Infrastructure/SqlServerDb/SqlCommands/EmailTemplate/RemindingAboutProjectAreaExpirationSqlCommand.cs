namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class RemindingAboutProjectAreaExpirationSqlCommand
{
    private readonly string _sqlCommand =
                 @"SELECT A.Id,
                      A.AgencyId, 
                      A.Title, 
                      A.StatusId, 
                      ISNULL(AD.FundingExpirationDate, NULL) AS ExpirationDate 
                      FROM [Flood].[FloodApplication]  A
               LEFT JOIN [Flood].[FloodApplicationAdminDetails] AD ON (A.Id = AD.ApplicationId)
               WHERE A.StatusId = 6 AND AD.FundingExpirationDate IS NOT NULL AND AD.FundingExpirationDate > GETDATE();";

    public RemindingAboutProjectAreaExpirationSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
