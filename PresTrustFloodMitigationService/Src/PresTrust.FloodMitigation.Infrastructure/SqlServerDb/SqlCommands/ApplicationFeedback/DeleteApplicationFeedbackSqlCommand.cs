namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteApplicationFeedbackSqlCommand
{
    private readonly string _sqlCommand =
       @"DELETE 
              FROM [Flood].[FloodApplicationFeedback]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteApplicationFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
