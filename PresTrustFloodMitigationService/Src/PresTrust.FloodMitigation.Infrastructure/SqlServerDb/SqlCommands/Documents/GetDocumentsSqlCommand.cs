namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetDocumentsSqlCommand
{
    private readonly string _sqlCommand =
         @"SELECT		 D.[Id]					AS	Id
		                ,D.[FileName]			AS	'FileName'
		                ,D.[Title]				AS  Title
                        ,D.[Description]		AS  'Description'
                        ,D.[UseInReport]		AS  UseInReport
			            ,D.[HardCopy]			AS	HardCopy
			            ,D.[Approved]			AS	Approved
			            ,D.[ReviewComment]		AS	ReviewComment
		                ,D.[DocumentTypeId]		AS	DocumentTypeId	
                        ,DT.[SectionId]			AS  SectionId
		                ,D.[ApplicationId]		AS  ApplicationId
            FROM		[Flood].[FloodDocument] D
            INNER JOIN	[Flood].[FloodDocumentType] DT
			            ON (DT.Id = D.DocumentTypeId)
            WHERE		DT.SectionId = CASE WHEN @p_SectionId > 0 THEN @p_SectionId ELSE DT.SectionId END
			            AND D.ApplicationId = @p_ApplicationId;";
    public GetDocumentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
