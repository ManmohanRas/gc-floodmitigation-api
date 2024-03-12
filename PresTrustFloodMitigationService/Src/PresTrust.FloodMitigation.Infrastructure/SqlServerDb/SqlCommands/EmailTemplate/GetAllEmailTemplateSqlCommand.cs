namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetAllEmailTemplateSqlCommand
{
    private readonly string _sqlCommand =
                 @"SELECT  [Id]
                          ,[TemplateCode]
	                      ,[Title]
                          ,[Subject]
	                      ,[Description]                       
                   FROM [Flood].[FloodEmailTemplate]
                   WHERE  IsActive = 1;";

    public GetAllEmailTemplateSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
