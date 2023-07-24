namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationSqlCommand
{
    private readonly string _sqlCommand =
            @" UPDATE [Flood].[FloodApplication]
               SET
                        [Title] = @p_Title,
                        [AgencyId] = @p_AgencyId,
                        [ApplicationTypeId] = @p_ApplicationTypeId,
                        [ApplicationSubTypeId] = @p_ApplicationSubTypeId,
                        [StatusId] = @p_StatusId,
                        [ExpirationDate] = DATEADD(YEAR, 3, GETDATE()),
                        [CreatedByProgramAdmin] = @p_CreatedByProgramAdmin,
                        [LastUpdatedBy] = @p_LastUpdatedBy,
                        [LastUpdatedOn] = GetDate()
               WHERE    [Id] = @p_ApplicationId;";

    public UpdateApplicationSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
