namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteDocumentSqlCommand
{
    private readonly string _sqlCommand =
              @" DELETE 
            FROM	[Flood].[FloodDocument]
            WHERE	Id = @p_Id;";

    public DeleteDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
