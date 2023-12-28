namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetTargetAreaDetailsByIdSqlCommand
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
						WHERE FFTA.[Id] = @p_Id;";

    public GetTargetAreaDetailsByIdSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
