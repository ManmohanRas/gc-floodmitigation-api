namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateParcelSurveySqlCommand
{
    private readonly string _sqlCommand =
   @"INSERT INTO [Flood].[FloodParcelSurvey]
                   ([ApplicationId]
                   ,[PamsPin]
                   ,[Surveyor]
                   ,[SurveyDate]
                   ,[LastRevision]
                   ,[DateCorrected]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@p_ApplicationId
                   ,@p_PamsPin
                   ,@p_Surveyor
                   ,@p_SurveyDate
                   ,@p_LastRevision
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateParcelSurveySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
