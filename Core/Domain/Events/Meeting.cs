using Core.Domain.ContactInfo;
using Core.Domain.Customers;
using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Events
{
    public class Meeting : BaseEntity
    {
        public Customer Customer { get; set; }
        public ICollection<User> Agents { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

    }
}
