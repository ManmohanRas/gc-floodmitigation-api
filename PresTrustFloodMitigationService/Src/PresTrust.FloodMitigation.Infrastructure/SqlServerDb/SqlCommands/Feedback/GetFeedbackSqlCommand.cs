namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public class GetFeedbackSqlCommand
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
							  ,[SectionId]
							  ,[Feedback]
							  ,[RequestForCorrection]
							  ,[CorrectionStatus]
							  ,[MarkRead]
							  ,[LastUpdatedBy]
							  ,[LastUpdatedOn]
				FROM		  [Flood].[FloodFeedback]
				WHERE		   ApplicationId = @p_ApplicationId
							   AND CorrectionStatus = CASE WHEN @p_CorrectionStatus = '' THEN CorrectionStatus ELSE @p_CorrectionStatus END;";

        public GetFeedbackSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
