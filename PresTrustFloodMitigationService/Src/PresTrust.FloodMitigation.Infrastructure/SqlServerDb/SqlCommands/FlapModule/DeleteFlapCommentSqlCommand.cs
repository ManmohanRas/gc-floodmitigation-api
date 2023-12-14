namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteFlapCommentSqlCommand
{
    private readonly string _sqlCommand =
        @" DELETE 
            FROM [Flood].[FloodAgencyFlapComment]
            WHERE Id=@p_Id;";

    public DeleteFlapCommentSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
