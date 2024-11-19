namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationParcelWarningsSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE   [Flood].[FloodApplicationParcel]
               SET      [WaitingApproved] = @p_WaitingApproved,
                        [RejectedApproved] = @p_RejectedApproved
               WHERE    [ApplicationId] = @p_ApplicationId and [PamsPin] = @p_PamsPin;";

    public UpdateApplicationParcelWarningsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
