namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeletePropertyFeedbackSqlCommand
{
    private readonly string _sqlCommand =
       @"DELETE 
              FROM [Flood].[FloodParcelFeedback]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";

    public DeletePropertyFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
