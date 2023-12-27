namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFlapTargetAreaSqlCommand
{
    private readonly string _sqlCommand =
     @" UPDATE [Flood].[FloodFlapTargetArea]
                   SET  [AgencyId] = @p_AgencyId
                       ,[TargetArea] = @p_TargetArea
                       ,[CreatedDate] = @p_CreatedDate
                       ,[LastUpdatedBy] = @p_LastUpdatedBy
                       ,[LastUpdatedOn] = @p_LastUpdatedOn
                WHERE  Id = @p_Id AND AgencyId = @p_AgencyId;";

    public UpdateFlapTargetAreaSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
