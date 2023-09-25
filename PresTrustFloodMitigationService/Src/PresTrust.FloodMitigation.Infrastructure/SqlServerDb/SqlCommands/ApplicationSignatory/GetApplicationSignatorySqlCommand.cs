namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationSignatorySqlCommand
{
    private readonly string _sqlCommand =
        @"  SELECT   [Id]
                    ,[ApplicationId]
                    ,[Designation]
                    ,[Title]
                    ,[SignedOn]
                    ,[LastUpdatedBy]
                    ,[LastUpdatedOn]
            FROM [Flood].[FloodApplicationSignatory]
            WHERE ApplicationId = @p_ApplicationId";


    public GetApplicationSignatorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
