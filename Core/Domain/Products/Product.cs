using Core.Domain.Logs;
using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string VersionNumber { get; set; }
        public string GuaranteeExpiresOn { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public ProductType Type { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

    }
}
