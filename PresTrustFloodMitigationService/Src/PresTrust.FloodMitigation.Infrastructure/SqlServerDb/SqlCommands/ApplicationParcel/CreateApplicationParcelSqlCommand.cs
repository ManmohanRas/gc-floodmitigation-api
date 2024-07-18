namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  INSERT INTO [Flood].[FloodApplicationParcel]
			    (
                    [ApplicationId],
                    [PamsPin],
                    [StatusId],
                    [IsLocked],
                    [WaitingApproved],
                    [RejectedApproved]
			    )
                VALUES
                    (
                    @p_ApplicationId,
                    @p_PamsPin,
                    @p_StatusId,
                    @p_IsLocked,
                    @p_WaitingApproved,
                    @p_RejectedApproved
                );";

    public CreateApplicationParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
