namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationParcelSoftCostStatusSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE   [Flood].[FloodApplicationParcel]
               SET      [IsSubmitted] = @p_IsSubmitted,
                        [IsApproved] = @p_IsApproved
               WHERE    [ApplicationId] = @p_ApplicationId and [PamsPin] = @p_PamsPin;";

    public UpdateApplicationParcelSoftCostStatusSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
