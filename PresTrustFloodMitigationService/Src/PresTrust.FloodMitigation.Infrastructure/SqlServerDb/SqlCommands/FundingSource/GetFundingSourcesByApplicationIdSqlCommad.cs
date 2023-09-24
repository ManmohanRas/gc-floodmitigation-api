namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFundingSourcesByApplicationIdSqlCommad
{
    private readonly string _sqlCommand =
         @"  SELECT FF.Id
                   ,FS.Id AS FundingSourceTypeId
                   ,FF.ApplicationId
                   ,CASE WHEN FS.Id < 7 THEN FS.Title
                    WHEN FS.Id = 7 THEN FF.Title
                    ELSE FF.Title
                    END AS Title
	               ,FF.Amount
	               ,FF.AwardDate
                    FROM [Flood].[FloodFundingSourceType] FS
                    LEFT JOIN [Flood].[FloodApplicationFinanceFund] FF  ON (FF.ApplicationId = @p_ApplicationId AND FS.Id = FF.FundingSourceTypeId);";
    public GetFundingSourcesByApplicationIdSqlCommad()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
