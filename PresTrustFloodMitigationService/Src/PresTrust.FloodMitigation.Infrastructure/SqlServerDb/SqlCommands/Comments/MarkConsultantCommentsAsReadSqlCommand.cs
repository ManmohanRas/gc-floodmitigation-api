namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class MarkConsultantCommentsAsReadSqlCommand
{
    private readonly string _sqlCommand =
       @"UPDATE		       [Hist].[HistConsultantComment]
             SET			   [MarkRead] = 1
             WHERE		       Id IN @p_CommentIds;";

    public MarkConsultantCommentsAsReadSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
