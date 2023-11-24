namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdatePropertyFeedbackSqlCommand
{
    /*
      DECLARE		@p_Id							AS  INT
                 ,@p_ApplicationId				AS	INT
                 ,@p_SectionId					AS	INT
                 ,@p_Feedback					    AS	VARCHAR(100)
                 ,@p_RequestForCorrection			AS	SMALLINT
                 ,@p_CorrectionStatus				AS	SMALLINT
                 ,@p_LastUpdatedBy				AS	VARCHAR(100);

      SET			@p_Id							=   5;
      SET			@p_ApplicationId				=   1;
      SET			@p_SectionId					=   1;
      SET			@p_Feedback					    =   'blah blah';
      SET			@p_RequestForCorrection			=   1;
      SET			@p_CorrectionStatus				=   1;
      SET			@p_LastUpdatedBy				=   'MG';
  */

    private readonly string _sqlCommand =
       @"UPDATE		       [Flood].[FloodParcelFeedback]
             SET			   [Feedback] = @p_Feedback
			                  ,[RequestForCorrection] = @p_RequestForCorrection
			                  ,[LastUpdatedBy] = @p_LastUpdatedBy
			                  ,[LastUpdatedOn] = GETDATE()
             WHERE		       Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";

    public UpdatePropertyFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
