namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetContactsSqlCommand
{
    private readonly string _sqlCommand =
        @"  SELECT [Id]   
                   ,[ApplicationId]
                   ,[ContactName]
                   ,[Agency]
                   ,[Email]
                   ,[MainNumber]
                   ,[AlternateNumber]
                   ,[SelectContact]
                   ,[LastUpdatedOn]
                   ,[LastUpdatedBy]
                FROM [Flood].[FloodContacts] 
                WHERE [ApplicationId] = @p_ApplicationId;"
        ;
    public GetContactsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
