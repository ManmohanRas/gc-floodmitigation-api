namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPrimaryContactsByApplicationIdSqlCommand
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
                FROM		[Flood].[FloodApplicationUser]
                WHERE		ApplicationId = @p_ApplicationId AND ISNULL(IsPrimaryContact,0) = 1;";
    public GetPrimaryContactsByApplicationIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
