namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  INSERT INTO [Flood].[FloodApplicationParcel]
			    (
                    [ApplicationId],
                    [PamsPin],
                    [StatusId],
                    [IsLocked]
			    )
                VALUES
                    (
                    @p_ApplicationId,
                    @p_PamsPin,
                    @p_StatusId,
                    @p_IsLocked
                );";

    public CreateApplicationParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
