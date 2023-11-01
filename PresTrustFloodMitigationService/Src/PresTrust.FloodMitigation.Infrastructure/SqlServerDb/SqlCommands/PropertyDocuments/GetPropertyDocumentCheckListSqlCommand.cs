namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class GetPropertyDocumentCheckListSqlCommand
    {
        private readonly string _sqlCommand = string.Empty;

        public GetPropertyDocumentCheckListSqlCommand()
        {

            _sqlCommand =
            @"SELECT		 ISNULL(D.[Id],0)		AS	Id
		                    ,D.[ApplicationId]		AS  ApplicationId
                            ,D.[Pamspin]            AS  'PamsPin'
		                    ,D.[FileName]			AS	'FileName'
		                    ,D.[Title]				AS  'Title'
                            ,D.[Description]		AS  'Description'
                            ,ISNULL(D.[UseInReport],0)		AS  UseInReport
			                ,ISNULL(D.[HardCopy],0)			AS	HardCopy
			                ,ISNULL(D.[Approved],0)			AS	Approved
			                ,D.[ReviewComment]		AS	ReviewComment
		                    ,DT.[Id]				AS	DocumentTypeId	
                            ,DT.[SectionId]			AS  SectionId
                    FROM		[Flood].[FloodParcelDocument] D
            INNER JOIN	[Flood].[FloodParcelDocumentType] DT
			                ON (DT.Id = D.DocumentTypeId  AND D.ApplicationId = @p_ApplicationId AND D.PamsPin = @p_Pamspin)";

        }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
