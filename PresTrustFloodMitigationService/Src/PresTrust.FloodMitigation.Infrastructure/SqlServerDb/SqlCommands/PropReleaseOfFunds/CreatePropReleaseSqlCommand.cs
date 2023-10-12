namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreatePropReleaseSqlCommand
{
//
//
//
//
//
//
    private readonly string _sqlCommand =
      @"INSERT INTO [Flood].[FloodParcelPayment]
                   ([ApplicationId]
                   ,[PamsPin]
                   ,[HardCostPaymentTypeId]
                   ,[HardCostPaymentDate]
                   ,[HardCostPaymentStatusId]
                   ,[SoftCostPaymentTypeId]
                   ,[SoftCostPaymentDate]
                   ,[SoftCostPaymentStatusId]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@P_ApplicationId
                   ,@P_Pamspin
                   ,@P_HardCostPaymentTypeId
                   ,@P_HardCostPaymentDate
                   ,@P_HardCostPaymentStatusId
                   ,@P_SoftCostPaymentTypeId
                   ,@P_SoftCostPaymentDate
                   ,@P_SoftCostPaymentStatusId
                   ,@P_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreatePropReleaseSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
