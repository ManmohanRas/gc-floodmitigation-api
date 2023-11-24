namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class MarkPropertyFeedbackAsReadSqlCommand
{
    private readonly string _sqlCommand =
      @"UPDATE		       [Flood].[FloodParcelFeedback]
             SET			   [MarkRead] = 1
             WHERE		       Id IN @p_FeedbackIds;";

    public MarkPropertyFeedbackAsReadSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
