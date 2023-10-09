namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdatePropReleaseOfFundsCommand
{
    private readonly string _sqlCommand =
     @"UPDATE [Flood].[FloodParcelPayment]
						SET
							[ApplicationId] = @p_ApplicationId,
							[PamsPin] = @p_PamsPin,
                            [HardCostPaymentTypeId] = @P_HardCostPaymentTypeId,
                            [HardCostPaymentDate] = @P_HardCostPaymentDate,
                            [HardCostPaymentStatusId] = @P_HardCostPaymentStatusId,
                            [SoftCostPaymentTypeId] = @P_SoftCostPaymentTypeId,
                            [SoftCostPaymentDate] = @P_SoftCostPaymentDate,
                            [SoftCostPaymentStatusId] = @P_SoftCostPaymentStatusId,
							[LastUpdatedBy] = @p_LastUpdatedBy,
							[LastUpdatedOn] = @P_LastUpdatedOn
                            WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin";

    public UpdatePropReleaseOfFundsCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
