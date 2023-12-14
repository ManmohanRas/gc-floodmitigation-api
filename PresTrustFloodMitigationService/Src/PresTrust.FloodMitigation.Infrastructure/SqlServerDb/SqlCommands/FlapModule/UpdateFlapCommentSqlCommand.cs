namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFlapCommentSqlCommand
{
    private readonly string _sqlCommand =
        @" UPDATE [Flood].[FloodAgencyFlapComment]
                   SET  [AgencyId] = @p_AgencyId
                       ,[Comment] = @p_Comment
                       ,[LastUpdatedBy] = @p_LastUpdatedBy
                       ,[LastUpdatedOn] = @p_LastUpdatedOn
                WHERE  Id = @p_Id AND AgencyId = @p_AgencyId;";

    public UpdateFlapCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
