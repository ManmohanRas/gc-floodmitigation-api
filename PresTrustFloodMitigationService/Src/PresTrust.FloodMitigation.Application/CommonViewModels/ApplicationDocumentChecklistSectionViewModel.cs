using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.CommonViewModels
{

    public class ApplicationDocumentChecklistSectionViewModel
    {
        public string Section { get; set; }
        public List<ApplicationDocumentChecklistDocTypeViewModel> ApplicationDocumentChecklistDocTypeItems { get; set; }
    }
}
