namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateSoftcostLineItemsSqlCommand
{
    private readonly string _sqlCommand=
        @" UPDATE [Flood].[FloodParcelSoftCost]
            SET     PamsPin = @p_PamsPin
                   ,SoftcostTypeId = @p_SoftcostTypeId
                   ,VendorName = @p_VendorName
                   ,InvoiceAmount = @p_InvoiceAmount
                   ,PaymentAmount = @p_PaymentAmount
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateSoftcostLineItemsSqlCommand()
    {

    }

    public override string ToString()
    {
        return _sqlCommand;
    }
} 

