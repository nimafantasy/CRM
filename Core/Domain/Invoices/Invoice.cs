using Core.Domain.ContactInfo;
using Core.Domain.Customers;
using Core.Domain.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Invoices
{
    public class Invoice : BaseEntity
    {
        public string Number { get; set; }
        public virtual ICollection<InvoiceItem> Items { get; set; }
        public string Description { get; set; }
        public virtual InvoiceType Type { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
