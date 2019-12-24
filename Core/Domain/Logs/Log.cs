using Core.Domain.Departments;
using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Logs
{
    public class Log : BaseEntity
    {
        public virtual User Submitter { get; set; }
        public LogPriority Priority { get; set; }
        public LogType Type { get; set; }
        public string PageUrl { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

    }
}
