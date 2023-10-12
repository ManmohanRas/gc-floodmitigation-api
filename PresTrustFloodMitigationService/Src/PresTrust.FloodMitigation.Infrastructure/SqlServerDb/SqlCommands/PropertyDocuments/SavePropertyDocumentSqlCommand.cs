namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public class SavePropertyDocumentSqlCommand
    {
    private readonly string _sqlCommand =
            @"INSERT INTO [Flood].[FloodParcelDocument]
                (
			                [FileName]
                           ,[Title]
                           ,[Description]
					       ,[HardCopy]		
					       ,[Approved]		
					       ,[ReviewComment]	
                           ,[DocumentTypeId]
                           ,[ApplicationId]
                           ,[PamsPin])
                     VALUES
                           (@p_FileName
                           ,@p_Title 
                           ,@p_Description
                           ,@p_HardCopy
                           ,@p_Approved
                           ,@p_ReviewComment
                           ,@p_DocumentTypeId 
                           ,@p_ApplicationId
                           ,@p_PamsPin
                );

                SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public SavePropertyDocumentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}

