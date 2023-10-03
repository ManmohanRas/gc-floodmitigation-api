namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationPaymentSqlCommand
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

    public GetApplicationPaymentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
