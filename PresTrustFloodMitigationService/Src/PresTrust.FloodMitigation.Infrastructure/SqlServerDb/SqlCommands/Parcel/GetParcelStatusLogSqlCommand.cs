namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelStatusLogSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT		[StatusId],
			            [StatusDate]
            FROM		[Flood].[FloodParcelStatusLog]
            WHERE		[ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin
            ORDER BY	[StatusDate] DESC;";

    public GetParcelStatusLogSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
