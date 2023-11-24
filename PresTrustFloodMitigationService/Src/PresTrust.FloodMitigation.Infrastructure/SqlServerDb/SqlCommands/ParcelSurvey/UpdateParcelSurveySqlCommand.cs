namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateParcelSurveySqlCommand
{
    private readonly string _sqlCommand =
       @"UPDATE [Flood].[FloodParcelSurvey]
             SET   [Surveyor] = @p_Surveyor
                  ,[SurveyDate] = @p_SurveyDate
                  ,[LastRevision] = @p_LastRevision
                  ,[DateCorrected] = @p_DateCorrected
			      ,[LastUpdatedBy] = @p_LastUpdatedBy
			      ,[LastUpdatedOn] = GETDATE()
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";

    public UpdateParcelSurveySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
