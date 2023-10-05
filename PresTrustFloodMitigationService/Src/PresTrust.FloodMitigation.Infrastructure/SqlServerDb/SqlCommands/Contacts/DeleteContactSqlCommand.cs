namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteContactSqlCommand
{
    private readonly string _sqlCommand =
       @" DELETE 
              FROM [Flood].[FloodContacts]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteContactSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
