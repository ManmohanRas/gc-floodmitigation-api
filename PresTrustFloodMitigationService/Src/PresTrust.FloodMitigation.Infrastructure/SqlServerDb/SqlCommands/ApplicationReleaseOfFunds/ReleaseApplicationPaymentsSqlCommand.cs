namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class ReleaseApplicationPaymentsSqlCommand
{
    private readonly string _sqlCommand =
      @" UPDATE [Flood].[FloodParcelPayment]
               SET  HardCostPaymentStatusId   =   @p_HardCostPaymentStatusId
                   ,SoftCostPaymentStatusId   =   @p_SoftCostPaymentStatusId
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND Pamspin = @p_Pamspin";

    public ReleaseApplicationPaymentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
