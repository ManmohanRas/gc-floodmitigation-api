namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetAllCommentsSqlCommand
{
    private readonly string _sqlCommand=
        @"  SELECT [Id]
                      ,[Comment]
                      ,[CommentTypeId]
                      ,[ApplicationId]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Flood].[FloodComment] 
                WHERE [ApplicationId] = @p_ApplicationId;"
        ;
    public GetAllCommentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
