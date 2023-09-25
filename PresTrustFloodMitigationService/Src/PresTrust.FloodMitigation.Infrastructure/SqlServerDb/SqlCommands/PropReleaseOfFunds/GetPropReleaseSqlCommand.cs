namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropReleaseSqlCommand
{
    private readonly string _sqlCommand =
         @" SELECT
	            AP.CAFNumber,
	            PP.HardCostPaymentTypeId,
	            PP.HardCostPaymentDate,
	            PP.HardCostPaymentStatus,
	            PP.SoftCostPaymentTypeId,
	            PP.SoftCostPaymentDate,
	            PP.SoftCostPaymentStatus,
	            PF.HardCostFMPAmt,
	            PF.SoftCostFMPAmt
            FROM
            Flood.FloodApplicationPayment AP
            JOIN
            Flood.FloodParcelPayment PP ON AP.ApplicationId = PP.ApplicationId
            JOIN
            Flood.FloodParcelFinance PF ON PP.ApplicationId = PF.ApplicationId AND PP.PamsPin = PF.PamsPin
         WHERE AP.ApplicationId = @p_ApplicationId AND PP.PamsPin = @p_PamsPin";

    public GetPropReleaseSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }

}