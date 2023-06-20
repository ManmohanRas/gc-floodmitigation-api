using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Domain.Entities
{
    public class FloodCommentsEntity
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Comment { get; set; }
        public int CommentTypeId { get; set; }
        public bool IsConsultantComment { get; set; }
        public bool MarkRead { get; set; } = false;
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
