namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class SaveApplicationDocumentSqlCommand
{
    private readonly string _sqlCommand =
            @"INSERT INTO [Flood].[FloodApplicationDocument]
                (
			                [FileName]
                           ,[Title]
                           ,[Description]
					       ,[HardCopy]		
					       ,[Approved]		
					       ,[ReviewComment]	
                           ,[DocumentTypeId]
                           ,[ApplicationId])
                     VALUES
                           (@p_FileName
                           ,@p_Title 
                           ,@p_Description
                           ,@p_HardCopy
                           ,@p_Approved
                           ,@p_ReviewComment
                           ,@p_DocumentTypeId 
                           ,@p_ApplicationId
                );

                SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public SaveApplicationDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
