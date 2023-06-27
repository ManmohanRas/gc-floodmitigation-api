namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.ApplicationUsers;

public class CreateApplicationUserSqlCommand
{
    private readonly string _sqlCommand =
    @"	INSERT INTO [Flood].[FloodApplicationUser]
                       ([ApplicationId]
                       ,[email]
                       ,[UserId]
                       ,[UserName]
                       ,[FirstName]
                       ,[LastName]
                       ,[Title]
                       ,[PhoneNumber]
                       ,[IsPrimaryContact]
                       ,[IsAlternateContact]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_ApplicationId
                       ,@p_Email 
                       ,@p_UserId
                       ,@p_UserName
                       ,@p_FirstName
                       ,@p_LastName
                       ,@p_Title
                       ,@p_PhoneNumber 
                       ,@p_PrimaryContact
                       ,@p_IsAlternateContact
                       ,@p_LastUpdatedBy
                       ,GETDATE())

				SELECT CAST( SCOPE_IDENTITY() AS INT);";
    public CreateApplicationUserSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
