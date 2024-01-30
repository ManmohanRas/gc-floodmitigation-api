namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationParcelWorkflowStatusSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE   [Flood].[FloodApplicationParcel]
               SET      [StatusId] = @p_StatusId,
                        [IsLocked] = @p_IsLocked
               WHERE    [ApplicationId] = @p_ApplicationId and [PamsPin] = @p_PamsPin;";

    public UpdateApplicationParcelWorkflowStatusSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
