namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationReleaseOfFundsSqlCommand
{
    private readonly string _sqlCommand =
      @" UPDATE [Flood].[FloodApplicationPayment]
               SET  ApplicationId = @p_ApplicationId
                   ,CAFNumber   =   @p_CAFNumber
                   ,CAFClosed   =   @p_CAFClosed
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId";

    public UpdateApplicationReleaseOfFundsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
