namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

class GetAllMunicipalCommentsSqlCommand
{
    private readonly string _sqlCommand =
       @"  SELECT [Id]
                      ,[Comment]
                      ,[AgencyId]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Flood].[FloodMunicipalComment] 
                WHERE [AgencyId] = @p_AgencyId;"
       ;
    public GetAllMunicipalCommentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}

