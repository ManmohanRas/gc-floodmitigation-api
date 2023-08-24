namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class RequestForApplicationCorrectionCommand
{
    private readonly string _sqlCommand =
           @"UPDATE		       [Flood].[ParcelFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
			                  ,[LastUpdatedOn] = GETDATE()
             WHERE		       ApplicationId = @p_ApplicationId 
                               Pamspin = @_Pamspin
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus, '') = 'PENDING';";

    public RequestForApplicationCorrectionCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
