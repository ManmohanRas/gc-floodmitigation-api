namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationPaymentSqlCommand
{
    private readonly string _sqlCommand =
      @" UPDATE [Flood].[FloodApplicationSignatory]
               SET  ApplicationId = @p_ApplicationId
                   ,CAFNumber   =   @p_CAFNumber
                   ,CAFClosed   =   @p_CAFClosed
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId";

    public UpdateApplicationPaymentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
