using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.CommonViewModels
{

    public class DocumentCheckListSectionViewModel
    {
        public string Section { get; set; }
        public List<DocumentCheckListDocTypeViewModel> DocumentCheckListDocTypeItems { get; set; }
    }
}
