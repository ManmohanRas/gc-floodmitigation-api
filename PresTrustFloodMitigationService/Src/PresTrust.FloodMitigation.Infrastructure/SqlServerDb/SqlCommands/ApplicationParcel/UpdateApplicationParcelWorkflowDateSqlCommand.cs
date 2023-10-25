namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationParcelWorkflowDateSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE [Flood].[FloodParcelProperty]
               SET  [LastUpdatedBy] = @p_LastUpdatedBy,
                        [LastUpdatedOn] = GetDate()
               WHERE    [ApplicationId] = @p_ApplicationId and [PamsPin] = @p_PamsPin;";

    public UpdateApplicationParcelWorkflowDateSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
