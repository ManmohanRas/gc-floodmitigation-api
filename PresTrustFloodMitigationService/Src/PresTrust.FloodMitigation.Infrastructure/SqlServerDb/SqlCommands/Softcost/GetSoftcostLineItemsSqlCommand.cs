namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetSoftcostLineItemsSqlCommand
{
    private readonly string _sqlCommand =
        @" SELECT
			FPSC.[Id],
			FPSC.[ApplicationId],
			FPSC.[PamsPin],
			FPSC.[SoftcostTypeId],
			FPSC.[VendorName],
			FPSC.[InvoiceAmount],
			FPSC.[PaymentAmount],
			FAF.[MatchPercent] AS CostShare
		FROM [Flood].[FloodParcelSoftCost] FPSC
		LEFT JOIN [Flood].[FloodApplicationFinance] FAF ON FPSC.[ApplicationId] = FAF.[ApplicationId]
		WHERE FPSC.[ApplicationId] = @p_ApplicationId AND FPSC.[PamsPin] = @p_PamsPin;";

    public GetSoftcostLineItemsSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
