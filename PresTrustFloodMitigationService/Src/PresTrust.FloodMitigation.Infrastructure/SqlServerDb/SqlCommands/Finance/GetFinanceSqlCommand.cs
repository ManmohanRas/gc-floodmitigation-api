namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFinanceSqlCommand
{
    private readonly string _sqlCommand =
                @"  SELECT   [Id]
                            ,[ApplicationId]
                            ,[MatchPercent]
                            ,[LastUpdatedBy]
                            ,[LastUpdatedOn]
                    FROM     [Flood].[FloodApplicationFinance]
                    WHERE  [ApplicationId] = @p_ApplicationId;";

    public GetFinanceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
