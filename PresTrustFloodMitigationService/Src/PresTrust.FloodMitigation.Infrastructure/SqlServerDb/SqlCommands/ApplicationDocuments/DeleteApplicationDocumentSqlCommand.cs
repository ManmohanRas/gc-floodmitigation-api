namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteApplicationDocumentSqlCommand
{
    private readonly string _sqlCommand =
              @" DELETE 
            FROM	[Flood].[FloodApplicationDocument]
            WHERE	Id = @p_Id;";

    public DeleteApplicationDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
