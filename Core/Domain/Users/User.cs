using Core.Domain.AccessGroups;
using Core.Domain.Logs;
using Core.Domain.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.ContactInfo;

namespace Core.Domain.Users
{
    public class User : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PersonelId { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

        public virtual ICollection<AccessGroup> AccessGroups { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual Department Department { get; set; }

    }

    
}
