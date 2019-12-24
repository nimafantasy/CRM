using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.AccessGroups
{
    public class AccessGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
