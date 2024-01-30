

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFundingDetailsSqlCommand
{
    private readonly string _sqlCommand =
     @" SELECT	
                           [Id]
                          ,[AllocationYear]
     		              ,[AllocationAmount]
			              ,[Interest] 
                          ,[AddedOrOmittedAmount]
                          ,[Comment]
			              ,[LastUpdatedBy]
			              ,[LastUpdatedOn]
        FROM    [Flood].[FloodAnnualFunding]";

    public GetFundingDetailsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
