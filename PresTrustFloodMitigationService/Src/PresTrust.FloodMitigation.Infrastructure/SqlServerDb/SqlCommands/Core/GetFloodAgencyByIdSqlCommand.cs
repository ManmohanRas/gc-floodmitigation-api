namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFloodAgencyByIdSqlCommand
{
    private readonly string _sqlCommand =
         @"  SELECT		AgencyId			AS Id
						   ,AgencyName
						   ,AgencyLabel
						   ,CASE AgencyType 
								WHEN 'Non-Profit' THEN 'nonprofit'
								WHEN 'Municipal' THEN 'municipality'
							END AS AgencyType
						   ,EntityType
						   ,EntityName
						   ,AddressLine1
						   ,AddressLine2
						   ,AddressLine3
						   ,City
						   ,State
						   ,ZipCode
				FROM		[Core].[View_AgencyEntities_FLOOD] 
				WHERE		AgencyId = @p_Id;";

    public GetFloodAgencyByIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
