namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeletePropertyCommentSqlCommand
{
    private readonly string _sqlCommand =
            @" DELETE 
              FROM [Flood].[FloodParcelComment]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";

    public DeletePropertyCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
