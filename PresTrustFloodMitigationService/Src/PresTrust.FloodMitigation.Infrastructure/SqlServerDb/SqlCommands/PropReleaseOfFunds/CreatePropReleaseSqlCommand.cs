namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreatePropReleaseSqlCommand
{
    private readonly string _sqlCommand =
      @"INSERT INTO [Flood].[FloodParcelPayment]
                   ([ApplicationId]
                   ,[Pamspin]
                   ,[ProjectAreaName]
                   ,[Property]
                   ,[ReimburesedHradCost]
                   ,[ReimburesedSoftCost]
                   ,[ReimburesedHradSoftCost]
                   ,[CAFNumber]
                   ,[FinalCost]
                   ,[PaymentMode]
                   ,[BalanceAmount]
                   ,[ReimbureseType]
                   ,[ReimbureseAmount]
                   ,[PaymentType]
                   ,[DateTransfareNeeded]
                   ,[LastUpdatedBy]
                   ,[LastUpdatedOn])
             VALUES
                   (@p_ApplicationId
                   ,@p_Pamspin
                   ,@P_ProjectAreaName
                   ,@_Property
                   ,@_ReimburesedSoftCost
                   ,@_ReimburesedHradSoftCost
                   ,@_ReimburesedHradCost
                   ,@_CAFNumber
                   ,@_FinalCost
                   ,@_PaymentMode
                   ,@_BalanceAmount
                   ,@_ReimbureseType
                   ,@_ReimbureseAmount
                   ,@_PaymentType
                   ,@_DateTransfareNeeded
                   ,@p_LastUpdatedBy
                   ,GETDATE());

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreatePropReleaseSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
