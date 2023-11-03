namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelStatusLogSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT		[StatusId],
			            [StatusDate]
            FROM		[Flood].[FloodParcelStatusLog]
            WHERE		[ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin AND [StatusId] IN (3,4,5,6,7,8,9,10)
            ORDER BY	[StatusDate] DESC;";

    public GetParcelStatusLogSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
