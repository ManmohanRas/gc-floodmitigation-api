namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class ResponseToRequestForPropertyCorrectionCommand
{

    private readonly string _sqlCommand =
       @"UPDATE		       [Flood].[FloodParcelFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
             WHERE		       ApplicationId = @p_ApplicationId  
                               AND PamsPin = @p_PamsPin
                               AND SectionId = @p_SectionId
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus,'') = 'REQUEST_SENT';";

    public ResponseToRequestForPropertyCorrectionCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
