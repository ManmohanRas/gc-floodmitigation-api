namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteFeedbackSqlCommand
{
    private readonly string _sqlCommand =
       @"DELETE 
              FROM [Flood].[FloodFeedback]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
