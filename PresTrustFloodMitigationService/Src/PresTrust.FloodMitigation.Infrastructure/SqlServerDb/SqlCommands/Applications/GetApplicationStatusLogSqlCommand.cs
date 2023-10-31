namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationStatusLogSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT		[StatusId],
			            [StatusDate]
            FROM		[Flood].[FloodApplicationStatusLog]
            WHERE		[ApplicationId] = @p_ApplicationId
            ORDER BY	[StatusDate] DESC;";

    public GetApplicationStatusLogSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
