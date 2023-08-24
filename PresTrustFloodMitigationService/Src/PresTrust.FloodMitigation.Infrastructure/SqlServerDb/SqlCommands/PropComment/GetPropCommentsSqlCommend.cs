namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropCommentsSqlCommend
{
    private readonly string _sqlCommand =
           @"  SELECT [Id]
                      ,[Comment]
                      ,[CommentTypeId]
                      ,[ApplicationId]
                      ,[Pamspin]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Flood].[ParcelComment] 
                WHERE [ApplicationId] = @p_ApplicationId;";

    public GetPropCommentsSqlCommend()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
