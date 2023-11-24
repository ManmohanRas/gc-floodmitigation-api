namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateSoftCostLineItemsSqlCommand
{
    private readonly string _sqlCommand=
        @" UPDATE [Flood].[FloodParcelSoftCost]
            SET     PamsPin = @p_PamsPin
                   ,SoftCostTypeId = @p_SoftCostTypeId
                   ,VendorName = @p_VendorName
                   ,InvoiceAmount = @p_InvoiceAmount
                   ,PaymentAmount = @p_PaymentAmount
                   ,IsSubmitted = @p_IsSubmitted
                   ,IsApproved = @p_IsApproved
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateSoftCostLineItemsSqlCommand()
    {

    }

    public override string ToString()
    {
        return _sqlCommand;
    }
} 

