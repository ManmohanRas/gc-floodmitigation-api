namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  INSERT INTO [Flood].[FloodApplicationParcel]
			    (
                    [ApplicationId],
                    [PamsPin],
                    [IsLocked]
			    )
                VALUES
                    (
                    @p_ApplicationId,
                    @p_PamsPin,
                    @p_IsLocked
                );";

    public CreateApplicationParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
