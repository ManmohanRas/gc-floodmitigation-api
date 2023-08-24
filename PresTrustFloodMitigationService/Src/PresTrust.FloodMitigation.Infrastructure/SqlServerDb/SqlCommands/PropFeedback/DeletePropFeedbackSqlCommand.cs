namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeletePropFeedbackSqlCommand
{
    private readonly string _sqlCommand =
       @"DELETE 
              FROM [Flood].[ParcelFeedback]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND Pamspin = @p_Pamspin;";

    public DeletePropFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
