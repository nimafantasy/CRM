using Core.Domain.ContactInfo;
using Core.Domain.Events;
using Core.Domain.Products;
using Core.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Customers
{
    public class Customer : BaseEntity
    {
        public virtual Company CustomerInfo { get; set; }
        public virtual ICollection<Product> PurchasedProducts { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Meeting> Sessions { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
