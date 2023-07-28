namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFinanceSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE [Flood].[FloodApplicationFinance]
                   SET [MatchPercent] = @p_MatchPercent
                      ,[LastUpdatedBy] = @p_LastUpdatedBy
                      ,[LastUpdatedOn] = @p_LastUpdatedOn
                WHERE  Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateFinanceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
