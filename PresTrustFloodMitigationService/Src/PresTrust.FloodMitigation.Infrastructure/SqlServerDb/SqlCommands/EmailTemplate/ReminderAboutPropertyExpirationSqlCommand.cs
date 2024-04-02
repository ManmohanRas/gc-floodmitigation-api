namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class ReminderAboutPropertyExpirationSqlCommand
{
    private readonly string _sqlCommand =
             @"SELECT 
                DISTINCT PSL.ApplicationId, 
                PSL.PamsPin,
                PSL.StatusDate,
                CONCAT(FP.[StreetNo], ' ', FP.[StreetAddress]) AS [PropertyAddress],
                PD.DocumentTypeId,
                PAD.GrantAgreementDate,
                CASE WHEN PD.DocumentTypeId IS NOT NULL THEN 1 ELSE 0 END AS IsDocumentUploaded
                FROM [Flood].[FloodParcelStatusLog] PSL 
                LEFT JOIN [Flood].[FloodParcel] FP ON (PSL.PamsPin = FP.PamsPin)
                LEFT JOIN [Flood].[FloodParcelAdminDetails] PAD ON (PSL.ApplicationId = PAD.ApplicationId AND PSL.PamsPin = PAD.PamsPin)
                LEFT JOIN [Flood].[FloodParcelDocument] PD ON (PSL.ApplicationId = PD.ApplicationId AND PSL.PamsPin = PD.PamsPin AND PD.DocumentTypeId IN(16,17,18))
                WHERE PSL.StatusId = 5;";

    public ReminderAboutPropertyExpirationSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
