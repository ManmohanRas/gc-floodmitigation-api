namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateSoftCostLineItemsSqlCommand
{
    private readonly string _sqlCommand=
        @" INSERT INTO [Flood].[FloodParcelSoftCost]
            (
              [ApplicationId]
             ,[PamsPin]
             ,[SoftCostTypeId]
             ,[VendorName]
             ,[InvoiceAmount]
             ,[PaymentAmount]
             ,[LastUpdatedBy]
             ,[LastUpdatedOn]
            )
        VALUES
            (
                @p_ApplicationId
               ,@p_PamsPin
               ,@p_SoftCostTypeId
               ,@p_VendorName
               ,@p_InvoiceAmount
               ,@p_PaymentAmount
               ,@p_LastUpdatedBy
               ,GetDate()
            );
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

    public CreateSoftCostLineItemsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
