namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class ReleasePaymentsSqlCommand
{
    private readonly string _sqlCommand =
      @" UPDATE [Flood].[FloodParcelPayment]
               SET  HardCostPaymentStatusId   =   @p_HardCostPaymentStatusId
                   ,SoftCostPaymentStatusId   =   @p_SoftCostPaymentStatusId
                   ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id;";

    public ReleasePaymentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
