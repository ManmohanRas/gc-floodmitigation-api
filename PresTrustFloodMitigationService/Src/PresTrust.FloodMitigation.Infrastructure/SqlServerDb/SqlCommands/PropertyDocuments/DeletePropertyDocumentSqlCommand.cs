namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeletePropertyDocumentSqlCommand
{
    private readonly string _sqlCommand =
              @"    DELETE  FROM
                    [Flood].[FloodParcelDocument]
                    WHERE   Id = @p_Id;";

    public DeletePropertyDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
