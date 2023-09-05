namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class IsValidPamsPinSqlCommand
{
    private readonly string _sqlCommand =
            @"  SELECT	COUNT(*) 
					FROM	[CORE].[Parcels]
					WHERE	[PAMS_PIN] = @p_PamsPin;";

    public IsValidPamsPinSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
