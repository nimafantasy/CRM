using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Addresses
{
    public class Address : BaseEntity
    {
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

    }
}
