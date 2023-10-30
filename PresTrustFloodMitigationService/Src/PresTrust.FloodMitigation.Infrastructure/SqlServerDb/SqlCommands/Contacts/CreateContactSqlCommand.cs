namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateContactSqlCommand
{
    private readonly string _sqlCommand=
         @"INSERT INTO [Flood].[FloodContacts]
              (
                 ApplicationId
                ,ContactName
                ,Agency
                ,Email
                ,MainNumber
                ,AlternateNumber
                ,SelectContact
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_ApplicationId
               ,@p_ContactName
               ,@p_Agency
               ,@p_Email
               ,@p_MainNumber
               ,@p_AlternateNumber
               ,@p_SelContact
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateContactSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
