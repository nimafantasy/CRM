using Core.Domain.Logs;
using Core.Domain.Requests;
using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Departments
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Request> PendingRequests { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public bool Active { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
