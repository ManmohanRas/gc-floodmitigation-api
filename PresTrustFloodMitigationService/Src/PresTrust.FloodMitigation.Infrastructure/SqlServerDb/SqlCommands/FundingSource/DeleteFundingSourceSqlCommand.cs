namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteFundingSourceSqlCommand
{
    private readonly string _sqlCommand =
            @"  DELETE 
                FROM	[Flood].[FloodFundingSourceType]
                WHERE   Id = @p_Id;";

    public DeleteFundingSourceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
