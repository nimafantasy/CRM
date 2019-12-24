using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Invoices
{
    public class InvoiceType : BaseEntity
    {
        public string Type { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
