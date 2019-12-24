using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Requests
{
    public class Attribute : BaseEntity
    {
        public string Name { get; set; }
        public virtual RequestType ParentType { get; set; }
    }
}
