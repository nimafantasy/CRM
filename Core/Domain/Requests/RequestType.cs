using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Requests
{
    public class RequestType : BaseEntity
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
