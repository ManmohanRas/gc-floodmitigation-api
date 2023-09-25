namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class MarkApplicationFeedbacksAsReadSqlCommand
{
    private readonly string _sqlCommand =
       @"UPDATE		       [Flood].[FloodApplicationFeedback]
             SET			   [MarkRead] = 1
             WHERE		       Id IN @p_FeedbackIds;";

    public MarkApplicationFeedbacksAsReadSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
