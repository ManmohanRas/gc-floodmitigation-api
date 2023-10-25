namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationParcelStatusLogSqlCommand
{
    private readonly string _sqlCommand =
            @"  DELETE FROM [Flood].[FloodParcelStatusLog]
                WHERE [ApplicationId] = @p_ApplicationId and [PamsPin] = @p_PamsPin AND [StatusId] = @p_StatusId;

                INSERT INTO [Flood].[FloodParcelStatusLog]
			    (
                    [ApplicationId],
                    [PamsPin],
                    [StatusId],
                    [StatusDate],
                    [Notes],
                    [LastUpdatedBy],
                    [LastUpdatedOn]
			    )
                VALUES
                    (
                    @p_ApplicationId,
                    @p_PamsPin,
                    @p_StatusId,
                    @p_StatusDate,
                    @p_Notes,
                    @p_LastUpdatedBy,
                    GetDate()
                );";

    public CreateApplicationParcelStatusLogSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
