namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteFlapDocumentSqlCommand
{
    private readonly string _sqlCommand =
          @" DELETE 
            FROM	[Flood].[FloodFlapDocument]
            WHERE	Id = @p_Id;";

    public DeleteFlapDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
