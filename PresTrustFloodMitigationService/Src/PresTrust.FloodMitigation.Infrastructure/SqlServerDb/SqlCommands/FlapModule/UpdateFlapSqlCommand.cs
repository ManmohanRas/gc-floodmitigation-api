namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFlapSqlCommand
{
    private readonly string _sqlCommand =
        @" UPDATE [Flood].[FloodAgencyFlap]
                   SET  [AgencyId] = @p_AgencyId
                       ,[FlapApproved] = @p_FlapApproved
                       ,[ApprovedDate] = @p_ApprovedDate
                       ,[LastRevisedDate] = @p_LastRevisedDate
                       ,[FlapMailToGrantee] = @p_FlapMailToGrantee
                       ,[LastUpdatedBy] = @p_LastUpdatedBy
                       ,[LastUpdatedOn] = @p_LastUpdatedOn
                WHERE  Id = @p_Id AND AgencyId = @p_AgencyId;";

    public UpdateFlapSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
