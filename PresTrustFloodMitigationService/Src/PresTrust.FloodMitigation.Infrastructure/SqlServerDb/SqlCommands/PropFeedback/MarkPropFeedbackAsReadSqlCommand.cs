namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class MarkPropFeedbackAsReadSqlCommand
{
    private readonly string _sqlCommand =
      @"UPDATE		       [Flood].[ParcelFeedback]
             SET			   [MarkRead] = 1
             WHERE		       Id IN @p_FeedbackIds;";

    public MarkPropFeedbackAsReadSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
