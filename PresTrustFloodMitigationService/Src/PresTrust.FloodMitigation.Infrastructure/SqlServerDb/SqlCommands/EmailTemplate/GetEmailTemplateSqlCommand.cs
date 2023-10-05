namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetEmailTemplateSqlCommand
{
    private readonly string _sqlCommand =
                 @" SELECT [Id]
	                      ,[Title]
                          ,[Subject]
                          ,[TemplateCode]
	                      ,[Description]                         
                   FROM [Hist].[HistEmailTemplate]
                   WHERE TemplateCode = @p_EmailTemplateCode AND IsActive = 1;";

    public GetEmailTemplateSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
