namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFundingSourceSqlCommand
{
    private readonly string _sqlCommand =
         @"INSERT INTO [Flood].[FloodApplicationFinanceFund]
						(
							 ApplicationId
							,FundingSourceTypeId
							,Title
							,Amount
							,AwardDate
							,LastUpdatedBy
							,LastUpdatedOn
						)

						VALUES
						(
							 @p_ApplicationId
							,@p_FundingSourceTypeId
							,@p_Title
							,@p_Amount
							,@p_AwardDate
							,@p_LastUpdatedBy  
							,@p_LastUpdatedOn	
						);

				  SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFundingSourceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
