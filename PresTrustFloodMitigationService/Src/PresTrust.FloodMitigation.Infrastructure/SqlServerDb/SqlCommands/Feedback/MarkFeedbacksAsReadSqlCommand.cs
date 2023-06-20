namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

internal class MarkFeedbacksAsReadSqlCommand
{
    private readonly string _sqlCommand =
       @"UPDATE		       [Hist].[HistFeedback]
             SET			   [MarkRead] = 1
             WHERE		       Id IN @p_FeedbackIds;";

    public MarkFeedbacksAsReadSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
