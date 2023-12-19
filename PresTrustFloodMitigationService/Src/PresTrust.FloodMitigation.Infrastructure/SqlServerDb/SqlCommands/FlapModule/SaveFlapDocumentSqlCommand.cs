namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class SaveFlapDocumentSqlCommand
{
    private readonly string _sqlCommand =
        @"INSERT INTO [Flood].[FloodFlapDocument]
                (
			                [AgencyId]
                           ,[FileName]
                           ,[Title]
                           ,[DocumentTypeId])
                     VALUES
                           (@p_AgencyId
                           ,@p_FileName
                           ,@p_Title
                           ,@p_DocumentTypeId
                );

                SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public SaveFlapDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
