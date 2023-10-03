namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFundingSourceSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE [Flood].[FloodApplicationFinanceFund]
                   SET [Title] =  @p_Title
                      ,[Amount] = @p_Amount
                      ,[AwardDate] = @p_AwardDate
                      ,[LastUpdatedBy] = @p_LastUpdatedBy
                      ,[LastUpdatedOn] = @p_LastUpdatedOn
                WHERE  Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateFundingSourceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
