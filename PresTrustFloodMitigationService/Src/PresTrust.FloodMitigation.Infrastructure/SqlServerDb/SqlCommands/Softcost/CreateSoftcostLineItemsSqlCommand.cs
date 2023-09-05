namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateSoftcostLineItemsSqlCommand
{
    private readonly string _sqlCommand=
        @" INSERT INTO [Flood].[FloodParcelSoftCost]
            (
              [ApplicationId]
             ,[PamsPin]
             ,[SoftcostTypeId]
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
               ,@p_SoftcostTypeId
               ,@p_VendorName
               ,@p_InvoiceAmount
               ,@p_PaymentAmount
               ,@p_LastUpdatedBy
               ,GetDate()
            );
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

    public CreateSoftcostLineItemsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
