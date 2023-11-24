namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropertyFeedbackSqlCommand
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
                              ,[PamsPin]
							  ,[SectionId]
							  ,[Feedback]
							  ,[RequestForCorrection]
							  ,[CorrectionStatus]
							  ,[MarkRead]
							  ,[LastUpdatedBy]
							  ,[LastUpdatedOn]
				FROM		  [Flood].[FloodParcelFeedback]
				WHERE		   ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin
							   AND CorrectionStatus = CASE WHEN @p_CorrectionStatus = '' THEN CorrectionStatus ELSE @p_CorrectionStatus END;";

    public GetPropertyFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
