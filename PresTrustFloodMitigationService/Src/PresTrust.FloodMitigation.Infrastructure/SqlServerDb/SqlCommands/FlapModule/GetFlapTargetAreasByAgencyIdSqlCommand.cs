namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFlapTargetAreasByAgencyIdSqlCommand
{
    private readonly string _sqlCommand =
                 @"	    SELECT
						FFTA.[Id],
						FFTA.[AgencyId],
						FFTA.[TargetArea],
                        FFTA.[CreatedDate],
						FFTA.[LastUpdatedBy],
						FFTA.[LastUpdatedOn]
						FROM [Flood].[FloodFlapTargetArea] FFTA
						WHERE FFTA.[AgencyId] = @p_AgencyId;";

    public GetFlapTargetAreasByAgencyIdSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
