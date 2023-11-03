namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationStatusLogSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT		[StatusId],
			            [StatusDate]
            FROM		[Flood].[FloodApplicationStatusLog]
            WHERE		[ApplicationId] = @p_ApplicationId AND [StatusId] IN (4,5,6,7,8,9)
            ORDER BY	[StatusDate] DESC;";

    public GetApplicationStatusLogSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
