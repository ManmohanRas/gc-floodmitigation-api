namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetSoftCostLineItemsSqlCommand
{
    private readonly string _sqlCommand =
        @"	SELECT
				FPSC.[Id],
				FPSC.[ApplicationId],
				FPSC.[PamsPin],
				FPSC.[SoftCostTypeId],
				FPSC.[VendorName],
				FPSC.[InvoiceAmount],
				FPSC.[PaymentAmount]
			FROM [Flood].[FloodParcelSoftCost] FPSC
			WHERE FPSC.[ApplicationId] = @p_ApplicationId AND FPSC.[PamsPin] = @p_PamsPin;";

    public GetSoftCostLineItemsSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
