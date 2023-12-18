namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFlapDetailsByAgencyIdSqlCommand
{
    private readonly string _sqlCommand =
    @"	SELECT
				FAF.[Id],
				FAF.[AgencyId],
				FAF.[FlapApproved],
				FAF.[ApprovedDate],
				FAF.[LastRevisedDate],
				FAF.[FlapMailToGrantee],
				FAF.[LastUpdatedBy],
                FAF.[LastUpdatedOn]
				FROM [Flood].[FloodAgencyFlap] FAF
			    WHERE FAF.[AgencyId] = @p_AgencyId;";

    public GetFlapDetailsByAgencyIdSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
