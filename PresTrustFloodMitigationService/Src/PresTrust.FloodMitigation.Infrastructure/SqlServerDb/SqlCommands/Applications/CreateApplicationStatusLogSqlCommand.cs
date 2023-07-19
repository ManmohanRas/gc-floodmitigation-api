namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationStatusLogSqlCommand
{
    private readonly string _sqlCommand =
            @"  DELETE FROM [Flood].[FloodApplicationStatusLog]
                WHERE [ApplicationId] = @p_ApplicationId AND [StatusId] = @p_StatusId;

                INSERT INTO [Flood].[FloodApplicationStatusLog]
			    (
                    [ApplicationId],
                    [StatusId],
                    [StatusDate],
                    [Notes],
                    [LastUpdatedBy],
                    [LastUpdatedOn]
			    )
                VALUES
                    (
                    @p_ApplicationId,
                    @p_StatusId,
                    @p_StatusDate,
                    @p_Notes,
                    @p_LastUpdatedBy,
                    GetDate()
                );";

    public CreateApplicationStatusLogSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
