namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateEmailTemplateSqlCommand
{
    private readonly string _sqlCommand =
            @"UPDATE     [Flood].[FloodEmailTemplate]
              SET        Title =    @p_Title
                        ,Description = @p_Description
                        ,TemplateCode = @p_TemplateCode
                        ,LastUpdatedBy = @p_LastUpdatedBy
                        ,LastUpdatedOn = GETDATE()
              WHERE Id = @p_Id;";

    public UpdateEmailTemplateSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
