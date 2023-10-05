namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationReleaseOfFundsSqlCommand
{
    private readonly string _sqlCommand =
                @"  SELECT   [Id]
                            ,[ApplicationId]
                            ,[CAFNumber]
                            ,[CAFClosed]
                            ,[LastUpdatedBy]
                            ,[LastUpdatedOn]
                    FROM     [Flood].[FloodApplicationPayment]
                    WHERE  [ApplicationId] = @p_ApplicationId;";

    public GetApplicationReleaseOfFundsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
