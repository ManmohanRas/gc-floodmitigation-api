namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationParcelSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT
	            FA.[ApplicationId],
	            FA.[PamsPin],
	            FA.[StatusId],
                FA.[IsLocked],
                FA.[IsSubmitted],
                FA.[IsApproved]
            FROM	[Flood].[FloodApplicationParcel] FA
            WHERE	FA.[ApplicationId] = @p_ApplicationId and FA.[PamsPin] = @p_PamsPin;";

    public GetApplicationParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
