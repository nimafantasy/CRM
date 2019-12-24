using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Requests
{
    public class AttributeValue :BaseEntity
    {
        public string Value { get; set; }
        public virtual Request ParentRequest { get; set; }
        public virtual Attribute RelatedAttribute { get; set; }
    }
}
