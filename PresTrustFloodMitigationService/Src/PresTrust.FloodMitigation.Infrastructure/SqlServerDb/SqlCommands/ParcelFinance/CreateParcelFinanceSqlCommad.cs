namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateParcelFinanceSqlCommad
{
    private readonly string _sqlCommand =
   @"INSERT INTO [Flood].[FloodParcelFinance]
                   ([ApplicationId]
                   ,[PamsPin]
                   ,[EstimatePurchasePrice]
                   ,[AdditionalSoftCostEstimate]
                   ,[AppraisedValue]
                   ,[AMV]
                   ,[TotalFEMABenifits]
                   ,[HomeOwnerDOBAffidavit]
                   ,[AppraisersFee]
                   ,[SurveyorsFee]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@p_ApplicationId
                   ,@p_PamsPin
                   ,@p_EstimatePurchasePrice
                   ,@p_AdditionalSoftCostEstimate
                   ,@p_AppraisedValue
                   ,@p_AMV
                   ,@p_TotalFEMABenifits
                   ,@p_HomeOwnerDOBAffidavit
                   ,@p_AppraisersFee
                   ,@p_SurveyorsFee
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateParcelFinanceSqlCommad() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
