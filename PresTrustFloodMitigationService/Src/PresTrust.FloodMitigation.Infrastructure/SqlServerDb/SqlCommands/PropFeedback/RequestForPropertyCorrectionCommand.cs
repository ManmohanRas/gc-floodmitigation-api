namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class RequestForPropertyCorrectionCommand
{
    private readonly string _sqlCommand =
           @"UPDATE		       [Flood].[ParcelFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
			                  ,[LastUpdatedOn] = GETDATE()
             WHERE		       ApplicationId = @p_ApplicationId 
                               Pamspin = @_Pamspin
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus, '') = 'PENDING';";

    public RequestForPropertyCorrectionCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
