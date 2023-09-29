namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdatePropReleaseOfFundsCommand
{
    private readonly string _sqlCommand =
     @"UPDATE [Flood].[FloodParcelTech]
						SET
							[ApplicationId] = @p_ApplicationId,
							[PamsPin] = @p_PamsPin,
                            [ProjectAreaName] = @P_ProjectAreaName,
                            [Property] = @_Property,
                            [ReimburesedHradCost] = @_ReimburesedSoftCost,
                            [ReimburesedSoftCost] = @_ReimburesedHradSoftCost,
                            [ReimburesedHradSoftCost] = @_ReimburesedHradCost,
                            [CAFNumber] = @_CAFNumber,
                            [FinalCost] = @_FinalCost,
                            [PaymentMode] = @_PaymentMode,
                            [BalanceAmount] = @_BalanceAmount,
                            [ReimbureseType] = @_ReimbureseType,
                            [ReimbureseAmount] = @_ReimbureseAmount,
                            [PaymentType] = @_PaymentType,
                            [DateTransfareNeeded] = @_DateTransfareNeeded,
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
