namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropertyCommentsSqlCommend
{
    private readonly string _sqlCommand =
           @"  SELECT [Id]
                      ,[Comment]
                      ,[CommentTypeId]
                      ,[ApplicationId]
                      ,[PamsPin]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Flood].[FloodParcelComment] 
                WHERE [ApplicationId] = @p_ApplicationId;";

    public GetPropertyCommentsSqlCommend()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
