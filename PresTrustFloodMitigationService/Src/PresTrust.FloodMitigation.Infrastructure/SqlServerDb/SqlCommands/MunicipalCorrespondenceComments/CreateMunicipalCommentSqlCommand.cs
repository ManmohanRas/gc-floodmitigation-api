namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateMunicipalCommentSqlCommand
{
    private readonly string _sqlCommand =
                    @"INSERT INTO [Flood].[FloodMunicipalComment]
              (
                 Comment
                ,AgencyId
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_Comment
               ,@p_AgencyId
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateMunicipalCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }


}

