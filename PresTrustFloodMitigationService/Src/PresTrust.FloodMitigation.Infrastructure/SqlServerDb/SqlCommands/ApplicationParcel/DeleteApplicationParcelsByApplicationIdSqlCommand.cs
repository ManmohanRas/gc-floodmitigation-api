namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteApplicationParcelsByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
        @" DELETE FROM [Flood].[FloodApplicationParcel]
           WHERE [ApplicationId] = @p_ApplicationId;";

    public DeleteApplicationParcelsByApplicationIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
