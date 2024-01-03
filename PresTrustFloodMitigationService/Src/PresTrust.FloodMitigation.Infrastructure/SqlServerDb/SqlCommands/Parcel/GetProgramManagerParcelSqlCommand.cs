namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetProgramManagerParcelSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT		*
			FROM		[Flood].[FloodParcel]
			WHERE		[Id]=@p_Id;";

    public GetProgramManagerParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
