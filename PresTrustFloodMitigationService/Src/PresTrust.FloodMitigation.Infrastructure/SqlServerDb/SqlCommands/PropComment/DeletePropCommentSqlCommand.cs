namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeletePropCommentSqlCommand
{
    private readonly string _sqlCommand =
            @" DELETE 
              FROM [Flood].[ParcelComment]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND Pamspin = @p_Pamspin;";

    public DeletePropCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
