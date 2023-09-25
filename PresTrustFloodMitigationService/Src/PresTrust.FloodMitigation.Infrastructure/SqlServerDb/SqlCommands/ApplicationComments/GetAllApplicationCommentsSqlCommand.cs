namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetAllApplicationCommentsSqlCommand
{
    private readonly string _sqlCommand=
        @"  SELECT [Id]
                      ,[Comment]
                      ,[CommentTypeId]
                      ,[ApplicationId]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Flood].[FloodApplicationComment] 
                WHERE [ApplicationId] = @p_ApplicationId;"
        ;
    public GetAllApplicationCommentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
