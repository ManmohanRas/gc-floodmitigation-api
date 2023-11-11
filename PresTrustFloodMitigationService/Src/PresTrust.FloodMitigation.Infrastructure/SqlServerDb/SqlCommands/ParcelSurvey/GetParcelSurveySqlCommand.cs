namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelSurveySqlCommand
{
    private readonly string _sqlCommand =
        @"  SELECT [Id]   
                   ,[ApplicationId]
                   ,[PamsPin]
                   ,[Surveyor]
                   ,[SurveyDate]
                   ,[LastRevision]
                   ,[DateCorrected]
                   ,[LastUpdatedOn]
                   ,[LastUpdatedBy]
                FROM [Flood].[FloodParcelSurvey]
                WHERE [ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin;"
        ;
    public GetParcelSurveySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
