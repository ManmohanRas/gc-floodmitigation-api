namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropFeedbackSqlCommand
{
    /*
      DECLARE	@p_ApplicationId AS INT,
               @p_CorrectionStatus AS VARCHAR(126);

      SET		@p_ApplicationId = 1;
      SET		@p_CorrectionStatus = '';
   */

    private readonly string _sqlCommand =
       @"  SELECT		   [Id]
							  ,[ApplicationId]
                              ,[Pamspin]
							  ,[SectionId]
							  ,[Feedback]
							  ,[RequestForCorrection]
							  ,[CorrectionStatus]
							  ,[MarkRead]
							  ,[LastUpdatedBy]
							  ,[LastUpdatedOn]
				FROM		  [Flood].[ParcelFeedback]
				WHERE		   ApplicationId = @p_ApplicationId
							   AND CorrectionStatus = CASE WHEN @p_CorrectionStatus = '' THEN CorrectionStatus ELSE @p_CorrectionStatus END;";

    public GetPropFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
