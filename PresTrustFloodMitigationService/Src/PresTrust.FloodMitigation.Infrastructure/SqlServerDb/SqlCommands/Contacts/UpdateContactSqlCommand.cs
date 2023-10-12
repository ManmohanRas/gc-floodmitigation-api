namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateContactSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Flood].[FloodContacts]
                SET ContactName = @p_ContactName
                    ,Agency = @p_Agency
                    ,Email = @p_Email
                    ,MainNumber = @p_MainNumber
                    ,AlternateNumber = @p_AlternateNumber
                    ,SelectContact = @p_SelContact
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateContactSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
