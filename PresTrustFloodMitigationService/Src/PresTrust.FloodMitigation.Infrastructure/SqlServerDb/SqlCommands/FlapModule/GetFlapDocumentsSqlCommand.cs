namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFlapDocumentsSqlCommand
{
    private readonly string _sqlCommand =
         @"SELECT		 D.[Id]					    AS	Id
		                ,D.[AgencyId]		        AS  AgencyId
		                ,D.[FileName]			    AS	'FileName'
		                ,D.[Title]				    AS  Title
                        ,D.[DocumentTypeId]		    AS	DocumentTypeId	
            FROM		[Flood].[FloodFlapDocument] D
			            WHERE D.AgencyId = @p_AgencyId;";
    public GetFlapDocumentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
