namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.ApplicationUsers;

public class DeleteApplicationUsersByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
        @" DELETE
            FROM  [Flood].[FloodApplicationUser]
            WHERE ApplicationId = @p_ApplicationId;";

    public DeleteApplicationUsersByApplicationIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
