namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFlapCommentsByAgencyIdSqlCommand
{
    private readonly string _sqlCommand =
            @"  SELECT
				FAFC.[Id],
				FAFC.[AgencyId],
				FAFC.[Comment]
			FROM [Flood].[FloodAgencyFlapComment] FAFC
			WHERE FAFC.[AgencyId] = @p_AgencyId;";

    public GetFlapCommentsByAgencyIdSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
