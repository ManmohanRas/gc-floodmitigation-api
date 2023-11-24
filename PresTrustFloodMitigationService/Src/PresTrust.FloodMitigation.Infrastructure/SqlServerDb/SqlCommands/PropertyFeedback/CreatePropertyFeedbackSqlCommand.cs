namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreatePropertyFeedbackSqlCommand
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
       @"INSERT INTO [Flood].[FloodParcelFeedback]
                   ([ApplicationId]
                   ,[PamsPin]
                   ,[SectionId]
                   ,[Feedback]
                   ,[RequestForCorrection]
                   ,[CorrectionStatus]
                   ,[MarkRead]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@p_ApplicationId
                   ,@p_PamsPin
                   ,@p_SectionId
                   ,@p_Feedback
                   ,@p_RequestForCorrection
                   ,@p_CorrectionStatus
                   ,@p_MarkRead
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreatePropertyFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
