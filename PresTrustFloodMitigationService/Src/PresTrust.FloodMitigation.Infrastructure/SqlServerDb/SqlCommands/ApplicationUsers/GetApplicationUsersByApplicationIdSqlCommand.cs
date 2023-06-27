namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationUsersByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
        @"      SELECT		 Id
			                ,ApplicationId
			                ,UserId
			                ,Email
			                ,UserName
                            ,FirstName
                            ,LastName
			                ,Title
			                ,PhoneNumber
			                ,IsPrimaryContact
                            ,IsAlternateContact
                FROM		[Flood].[FloodApplicationUser]
                WHERE		ApplicationId = @p_ApplicationId;";
    public GetApplicationUsersByApplicationIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
