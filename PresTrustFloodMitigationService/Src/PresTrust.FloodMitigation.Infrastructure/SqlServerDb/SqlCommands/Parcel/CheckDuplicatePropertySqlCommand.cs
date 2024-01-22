namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CheckDuplicatePropertySqlCommand
{
    private readonly string _sqlCommand =
            @"  IF(@p_Id > 0)
					BEGIN
						SELECT
							CASE WHEN COUNT(Id) > 0 THEN 1 ELSE 0 END AS [Result]
						FROM [Flood].[FloodParcel] WHERE PamsPin = @p_PamsPin AND [Id] != @p_Id;
					END
				ELSE
					BEGIN
						SELECT
							CASE WHEN COUNT(Id) > 0 THEN 1 ELSE 0 END AS [Result]
						FROM [Flood].[FloodParcel] WHERE PamsPin = @p_PamsPin;
					END;";

    public CheckDuplicatePropertySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
