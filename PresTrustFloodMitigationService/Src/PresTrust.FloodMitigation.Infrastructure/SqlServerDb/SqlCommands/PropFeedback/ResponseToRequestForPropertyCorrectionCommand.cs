namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class ResponseToRequestForPropertyCorrectionCommand
{

    private readonly string _sqlCommand =
       @"UPDATE		       [Flood].[ParcelFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
             WHERE		       ApplicationId = @p_ApplicationId  
                               AND Pamspin = @_Pamspin
                               AND SectionId = @p_SectionId
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus,'') = 'REQUEST_SENT';";

    public ResponseToRequestForPropertyCorrectionCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
