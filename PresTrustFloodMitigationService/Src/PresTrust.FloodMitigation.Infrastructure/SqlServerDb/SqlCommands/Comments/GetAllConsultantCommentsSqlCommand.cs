namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Comments;

public class GetAllConsultantCommentsSqlCommand
{
    private readonly string _sqlCommand =
       @"  SELECT [Id]
                      ,[Comment]
                      ,[CommentTypeId]
                      ,[ApplicationId]
	                  ,[MarkRead]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Hist].[HistConsultantComment] 
                WHERE [ApplicationId] = @p_ApplicationId;";

    public GetAllConsultantCommentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
