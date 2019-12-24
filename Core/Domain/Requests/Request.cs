using Core.Domain.Customers;
using Core.Domain.Departments;
using Core.Domain.Invoices;
using Core.Domain.Logs;
using Core.Domain.Products;
using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Requests
{
    public class Request : BaseEntity
    {
        public RequestStatus Status { get; set; }
        public virtual RequestType Type { get; set; }
        public ICollection<AttributeValue> AttributeValues { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Department ResponsibleDepartment { get; set; }
        public virtual Invoice Invoice { get; set;}
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<User> Responders { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

    }
}
