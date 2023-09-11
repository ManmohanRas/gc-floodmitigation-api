namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Feedback
{
    public class ResponseToRequestForApplicationCorrectionCommand
    {
        private readonly string _sqlCommand =
        @"UPDATE		       [Flood].[FloodFeedback]
             SET			   [CorrectionStatus] = @p_CorrectionStatus
             WHERE		       ApplicationId = @p_ApplicationId    
                               AND SectionId = @p_SectionId
                               AND ISNULL(RequestForCorrection,0) = 1 
                               AND ISNULL(CorrectionStatus,'') = 'REQUEST_SENT';";

        public ResponseToRequestForApplicationCorrectionCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
