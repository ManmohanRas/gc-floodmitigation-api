namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateParcelFinanceSqlCommand
{
    private readonly string _sqlCommand =
       @"UPDATE		       [Flood].[FloodParcelFinance]
             SET			   [EstimatePurchasePrice] = @p_EstimatePurchasePrice
			                  ,[AdditionalSoftCostEstimate] = @p_AdditionalSoftCostEstimate
                              ,[AppraisedValue] = @p_AppraisedValue
                              ,[AMV] = @p_AMV
                              ,[TotalFEMABenifits] = @p_TotalFEMABenifits
                              ,[HomeOwnerDOBAffidavit] = @p_HomeOwnerDOBAffidavit
                              ,[AppraisersFee] = @p_AppraisersFee
                              ,[SurveyorsFee] = @p_SurveyorsFee
			                  ,[LastUpdatedBy] = @p_LastUpdatedBy
			                  ,[LastUpdatedOn] = GETDATE()
             WHERE		       Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateParcelFinanceSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
