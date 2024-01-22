namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
 class DeleteMunicipalCommentSqlCommand
 { 
 
    private readonly string _sqlCommand =
    @" DELETE 
              FROM [Flood].[FloodMunicipalComment]
              WHERE Id = @p_Id AND AgencyId = @p_AgencyId;";

    public DeleteMunicipalCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }

 }

