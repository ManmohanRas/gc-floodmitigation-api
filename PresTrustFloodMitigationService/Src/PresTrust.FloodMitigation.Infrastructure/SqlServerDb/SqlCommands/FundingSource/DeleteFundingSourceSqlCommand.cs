namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteFundingSourceSqlCommand
{
    private readonly string _sqlCommand =
            @"  DELETE 
                FROM	[Flood].[FloodFinanceFund]
                WHERE   Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteFundingSourceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
