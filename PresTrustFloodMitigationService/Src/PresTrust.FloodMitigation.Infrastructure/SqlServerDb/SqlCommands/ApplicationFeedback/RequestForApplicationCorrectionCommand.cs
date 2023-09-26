namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class RequestForApplicationCorrectionCommand
{
    private readonly string _sqlCommand =
          @"UPDATE		       [Flood].[FloodApplicationFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
			                  ,[LastUpdatedOn] = GETDATE()
             WHERE		       ApplicationId = @p_ApplicationId 
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus, '') = 'PENDING';";

    public RequestForApplicationCorrectionCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
