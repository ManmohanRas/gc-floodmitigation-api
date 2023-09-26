using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Documents
{
     public class GetDocumentCheckListSqlCommand
    {
        private readonly string _sqlCommand = string.Empty;

        public GetDocumentCheckListSqlCommand(bool hasCOEDocument)
        {
            
                _sqlCommand =
                @"SELECT		 ISNULL(D.[Id],0)		AS	Id
		                ,D.[ApplicationId]		AS  ApplicationId
		                    ,D.[FileName]			AS	'FileName'
		                    ,D.[Title]				AS  Title
                            ,D.[Description]		AS  'Description'
                            ,ISNULL(D.[UseInReport],0)		AS  UseInReport
			                ,ISNULL(D.[HardCopy],0)			AS	HardCopy
			                ,ISNULL(D.[Approved],0)			AS	Approved
			                ,D.[ReviewComment]		AS	ReviewComment
		                    ,DT.[Id]				AS	DocumentTypeId	
                            ,DT.[SectionId]			AS  SectionId
			                ,0						AS  IsPropertyDocument
                    FROM		[Flood].[FloodDocument] D
            INNER JOIN	[Flood].[FloodDocumentType] DT
			                ON (DT.Id = D.DocumentTypeId  AND D.ApplicationId = @p_ApplicationId)";
            
        }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
