using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.CommonViewModels
{
    public class PropertyDocumentChecklistDocTypeViewModel
    {
        public int Id { get; set; }
        public string? PamsPin { get; set; }
        public int ApplicationId { get; set; }
        public string? Section { get; set; }
        public string? DocumentType { get; set; }
        public List<PropertyDocumentViewModel> Documents { get; set; }
    }
}
