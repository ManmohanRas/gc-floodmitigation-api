using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Queries.GetDocumentChecklist
{
    public class GetDocumentCheckListQuery : IRequest<IEnumerable<DocumentCheckListSectionViewModel>>
    {
        public int ApplicationId { get; set; }
    }
}
