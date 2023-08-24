namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class ResponseToRequestForApplicationCorrectionCommand
{

    private readonly string _sqlCommand =
       @"UPDATE		       [Flood].[ParcelFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
             WHERE		       ApplicationId = @p_ApplicationId  
                               Pamspin = @_Pamspin
                               AND SectionId = @p_SectionId
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus,'') = 'REQUEST_SENT';";

    public ResponseToRequestForApplicationCorrectionCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
